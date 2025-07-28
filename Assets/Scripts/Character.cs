using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TreeEditor;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    protected Direction Direction;
    protected Team Team;

    protected float HitPoint;
    protected float HitPointMax;
    protected float MoveSpeed;
    protected float SpecialPoint;
    protected float SpecialPointMax;

    protected List<CharacterEffect> _effects = new List<CharacterEffect>();
    protected CharacterState _state;

    protected TimeSpan _basicAttackCoolTime;
    protected TimeSpan[] _skillCoolTime;
    protected TimeSpan _basicAttackRemainCoolTime;
    protected TimeSpan[] _skillRemainCoolTime;


    private Dictionary<int, TimeSpan> _projectileHitTime;
    private TimeSpan _lastProjectileHitTimeCleanTime;


    public virtual void ManualStart(Team team)
    {
        InitializeBaseStats();
        Team = team;
        _effects = new List<CharacterEffect>();

        _basicAttackRemainCoolTime = TimeSpan.Zero;
        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount];

        _projectileHitTime = new Dictionary<int, TimeSpan>();
        _lastProjectileHitTimeCleanTime = TimeSpan.Zero;
    }

    public virtual void ManualUpdate()
    {
        UpdateState();
        CleanEffects();
        UpdateCoolTime();
        if (GameController.Instance.GetPlayerTime(Team) - _lastProjectileHitTimeCleanTime > TimeSpan.FromSeconds(0.3f))
        {
            CleanProjectileHitTime();
            _lastProjectileHitTimeCleanTime = GameController.Instance.GetPlayerTime(Team);
        }
    }

    private void UpdateState()
    {
        switch (_state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Dash dash:
                if (dash.endTime < GameController.Instance.GetPlayerTime(Team))
                {
                    _state = new CharacterState.Idle();
                }
                break;
            case CharacterState.Rush rush:
                break;
            case CharacterState.Drive drive:
                if (drive.endTime < GameController.Instance.GetPlayerTime(Team))
                {
                    _state = new CharacterState.Idle();
                }
                break;
        }
    }


    public void AddEffect(CharacterEffect effect)
    {
        effect.ManualStart();
        _effects.Add(effect);
    }

    private void CleanEffects()
    {
        _effects.RemoveAll(effect => effect.IsEnded());
    }

    private void CleanProjectileHitTime()
    {
        var keysToRemove = new List<int>();
        foreach (var pair in _projectileHitTime)
        {
            if (pair.Value < GameController.Instance.GetPlayerTime(Team))
            {
                keysToRemove.Add(pair.Key);
            }
        }
        foreach (var key in keysToRemove)
        {
            _projectileHitTime.Remove(key);
        }
    }

    private void UpdateCoolTime()
    {
        _basicAttackRemainCoolTime -= GameController.Instance.GetPlayerDeltaTime(Team);
        for (int i = 0; i < GameConst.SkillCount; i++)
        {
            _skillRemainCoolTime[i] -= GameController.Instance.GetPlayerDeltaTime(Team);
        }
    }

    public float HitPointRatio()
    {
        return (float)HitPoint / (float)HitPointMax;
    }


    public Vector2 GetPosition()
    {
        return transform.localPosition;
    }

    public Direction GetDirection()
    {
        return Direction;
    }

    public Team GetTeam()
    {
        return Team;
    }


    public void AddHitPoint(float amount)
    {
        HitPoint = Mathf.Clamp(HitPoint + amount, 0, HitPointMax);
    }

    public void AddSpecialPoint(float amount)
    {
        SpecialPoint = Mathf.Clamp(SpecialPoint + amount, 0, SpecialPointMax);
    }

    protected abstract void InitializeBaseStats();

    public virtual void OnPressDirection(Direction direction)
    {
        if (!IsMoveAble()) return;

        GraduallyMove(direction, GetMoveSpeed() * Time.deltaTime);

        //_rigidbody2D.MovePosition(transform.localPosition + (GetMoveSpeed() * Time.deltaTime * Utils.DirectionToVector3(direction)));

        Direction = direction;
    }


    public virtual bool OnPressBasicAttack()
    {
        if (!IsBasicAttackAble()) return false;
        _basicAttackRemainCoolTime = _basicAttackCoolTime;
        return true;
    }

    public virtual bool OnPressSkill0()
    {
        if (!IsSkillAble(0))
        {
            Debug.Log("Skill0 is not able to use");
            return false;
        }
        _skillRemainCoolTime[0] = _skillCoolTime[0];
        return true;
    }

    public virtual bool OnPressSkill1()
    {
        if (!IsSkillAble(1)) return false;
        _skillRemainCoolTime[1] = _skillCoolTime[1];
        return true;
    }

    public virtual bool OnPressSkill2()
    {
        if (!IsSkillAble(2)) return false;
        _skillRemainCoolTime[2] = _skillCoolTime[2];
        return true;
    }

    public virtual void OnHitByProjectile(Projectile projectile)
    {
        if (!CanHitByProjectile(projectile)) return;
        _projectileHitTime[projectile.ProjectileID] = GameController.Instance.GetPlayerTime(Team) + projectile.HitIntervalTime;
    }

    public bool CanHitByProjectile(Projectile projectile)
    {
        if (projectile.Team == Team) return false;

        return !_projectileHitTime.ContainsKey(projectile.ProjectileID);
    }

    private bool IsMoveAble()
    {
        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.Stun) return false;
            if (effect.EffectCategory is CharacterEffectCategory.Bond) return false;
        }

        if (_state is CharacterState.Dash) return false;

        return true;
    }

    private bool IsBasicAttackAble()
    {
        if (_basicAttackRemainCoolTime > TimeSpan.Zero) return false;

        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.Stun) return false;
        }

        if (_state is CharacterState.Drive) return false;

        return true;
    }

    private bool IsSkillAble(int skillIndex)
    {
        if (_skillRemainCoolTime[skillIndex] > TimeSpan.Zero) return false;

        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.Stun) return false;
            if (effect.EffectCategory is CharacterEffectCategory.Silence) return false;
        }

        if (_state is CharacterState.Drive) return false;

        return true;
    }

    private float GetMoveSpeed()
    {
        var ratio = 1f;
        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.MoveSpeed moveSpeed)
            {
                ratio *= moveSpeed.Rate;
            }
        }
        return MoveSpeed * ratio;
    }

    public float GetBasicAttackCoolTimeRatio()
    {
        return (float)_basicAttackRemainCoolTime.TotalSeconds / (float)_basicAttackCoolTime.TotalSeconds;
    }

    public float GetSkillCoolTimeRatio(int skillIndex)
    {
        return (float)_skillRemainCoolTime[skillIndex].TotalSeconds / (float)_skillCoolTime[skillIndex].TotalSeconds;
    }

    protected void ChangeStateDash(Direction direction, float power, bool isWallPassable, TimeSpan dashTime)
    {
        _state = new CharacterState.Dash(direction, power, isWallPassable, GameController.Instance.GetPlayerTime(Team) + dashTime);
    }

    protected void ChangeStateRush(Direction direction, float power, bool isWallPassable)
    {
        _state = new CharacterState.Rush(direction, power, isWallPassable);
    }

    protected void ChangeStateDrive(bool isWallPassable, TimeSpan driveTime)
    {
        _state = new CharacterState.Drive(isWallPassable, GameController.Instance.GetPlayerTime(Team) + driveTime);
    }




    private bool IsSomething(Direction direction)
    {
        int layerMask = LayerMask.GetMask("Wall");
        
        //RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(direction), 0.3f, layerMask);

        if ((direction == Direction.Up) || (direction == Direction.UpLeft) || (direction == Direction.UpRight))
        {
            RaycastHit2D hitUp = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(Direction.Up), 0.5f, layerMask);
            if (hitUp.collider != null) return true;
        }

        if ((direction == Direction.Down) || (direction == Direction.DownLeft) || (direction == Direction.DownRight))
        {
            RaycastHit2D hitDown = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(Direction.Down), 0.5f, layerMask);
            if (hitDown.collider != null) return true;
        }

        if ((direction == Direction.Left) || (direction == Direction.UpLeft) || (direction == Direction.DownLeft))
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(Direction.Left), 0.3f, layerMask);
            if (hitLeft.collider != null) return true;
        }
        
        if ((direction == Direction.Right) || (direction == Direction.UpRight) || (direction == Direction.DownRight))
        {
            RaycastHit2D hitRight = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(Direction.Right), 0.3f, layerMask);
            if (hitRight.collider != null) return true;
        }

        return false;
    }

    private void GraduallyMove(Direction direction, float power)
    {
        float step = 0.05f;

        if (IsSomething(direction))
        {
            return;
        }

        transform.localPosition += Mathf.Min(power, step) * Utils.DirectionToVector3(direction);

        //_rigidbody2D.MovePosition(transform.localPosition + Mathf.Min(power, step) * Utils.DirectionToVector3(direction));

        if (power > step)
        {
            GraduallyMove(direction, power - step);
        }
    }

    public Transform GetPlayerTransform()
    {
        return transform;
    }
    
    


}


public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

public abstract record CharacterState
{
    public sealed record Idle() : CharacterState;
    public sealed record Dash(Direction direction, float power, bool isWallPassable, TimeSpan endTime) : CharacterState;
    public sealed record Rush(Direction direction, float power, bool isWallPassable) : CharacterState;

    public sealed record Drive(bool isWallPassable, TimeSpan endTime) : CharacterState;
    


}
