using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TreeEditor;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _spinnedTransform;
    [SerializeField] private Transform _bodyTransform;

    
    protected Direction Direction;
    protected Team Team;

    protected float _hitPoint;
    protected float _hitPointMax;
    protected float _moveSpeed;
    protected float _specialPoint;
    protected float _speicalPointMax;

    protected List<CharacterEffect> _effects = new List<CharacterEffect>();
    protected CharacterState _state;

    protected TimeSpan _basicAttackCoolTime;
    protected TimeSpan[] _skillCoolTime;
    protected TimeSpan _basicAttackRemainCoolTime;
    protected TimeSpan[] _skillRemainCoolTime;


    private Dictionary<int, TimeSpan> _projectileHitTime;
    private TimeSpan _lastProjectileHitTimeCleanTime;

    private bool _isWallPassable;
    private bool _isFlying;



    public virtual void ManualStart(Team team)
    {
        InitializeBaseStats();
        Team = team;
        gameObject.layer = LayerMask.NameToLayer(Team == Team.Left ? "LeftCharacter" : "RightCharacter");
        
        _effects = new List<CharacterEffect>();

        _basicAttackRemainCoolTime = TimeSpan.Zero;
        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount];

        _projectileHitTime = new Dictionary<int, TimeSpan>();
        _lastProjectileHitTimeCleanTime = TimeSpan.Zero;

        ChangeStateIdle();
    }

    public virtual void ManualUpdate()
    {
        UpdateState();
        UpdateByState();
        UpdateBodyTransform();
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
                    ChangeStateIdle();
                }
                break;
            case CharacterState.Rush rush:
                break;
            case CharacterState.Drive drive:
                if (drive.endTime < GameController.Instance.GetPlayerTime(Team))
                {
                    ChangeStateIdle();
                }
                break;
            case CharacterState.ForcedMove forcedMove:
                if (forcedMove.endTime < GameController.Instance.GetPlayerTime(Team))
                {
                    ChangeStateIdle();
                }
                break;
        }
    }

    private void UpdateByState()
    {
        switch (_state)
        {
            case CharacterState.Idle:
                break;
            case CharacterState.Dash dash:
                GraduallyMove(dash.direction, dash.power * Time.deltaTime);
                break;
            case CharacterState.Rush rush:
                GraduallyMove(rush.direction, rush.power * Time.deltaTime);
                break;
            case CharacterState.Drive drive:
                break;
            case CharacterState.ForcedMove forcedMove:
                GraduallyMove(forcedMove.direction, forcedMove.power * Time.deltaTime);
                break;
        }
    }

    private void UpdateBodyTransform()
    {
        float maxHeight = 2.0f;
        float maxScale = 2.0f;
        float height = 0.0f;
        float scale = 1.0f;
        TimeSpan currentTime = GameController.Instance.GetPlayerTime(Team);
        float progress;

        if (!_isFlying)
        {
            height = 0.0f;
            scale = 1.0f;
        }
        else
        {
            switch (_state)
            {
                case CharacterState.Idle:
                    height = maxHeight;
                    scale = maxScale;
                    break;
                case CharacterState.Dash dash:
                    GraduallyMove(dash.direction, dash.power * Time.deltaTime);
                    progress = (float)(currentTime - dash.startTime).TotalSeconds / (float)(dash.endTime - dash.startTime).TotalSeconds;
                    height = (-4) * maxHeight * progress * (progress - 1);
                    scale = (-4) * (maxHeight - 1) * progress * (progress - 1) + 1;
                    break;
                case CharacterState.Rush rush:
                    GraduallyMove(rush.direction, rush.power * Time.deltaTime);
                    height = maxHeight;
                    scale = maxScale;
                    break;
                case CharacterState.Drive drive:
                    progress = (float)(currentTime - drive.startTime).TotalSeconds / (float)(drive.endTime - drive.startTime).TotalSeconds;
                    height = (-4) * maxHeight * progress * (progress - 1);
                    scale = (-4) * (maxHeight - 1) * progress * (progress - 1) + 1;
                    break;
                case CharacterState.ForcedMove forcedMove:
                    GraduallyMove(forcedMove.direction, forcedMove.power * Time.deltaTime);
                    progress = (float)(currentTime - forcedMove.startTime).TotalSeconds / (float)(forcedMove.endTime - forcedMove.startTime).TotalSeconds;
                    height = (-4) * maxHeight * progress * (progress - 1);
                    scale = (-4) * (maxHeight - 1) * progress * (progress - 1) + 1;
                    break;
            }
        }

        _bodyTransform.localPosition = new Vector3(0.0f, height, 0.0f);
        _bodyTransform.localScale = new Vector3(scale, scale, scale);
    }

    private void UpdateWallPassable(bool passable)
    {
        if (_isWallPassable == passable) return;

        _isWallPassable = passable;
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer(Team == Team.Left ? "LeftCharacter" : "RightCharacter"),
            LayerMask.NameToLayer("Wall"), passable);
    }

    private void UpdateFlying(bool flying)
    {
        if (_isFlying == flying) return;

        _isFlying = flying;
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer(Team == Team.Left ? "LeftCharacter" : "RightCharacter"),
            LayerMask.NameToLayer(Team == Team.Left ? "RightProjectile" : "LeftProjectile"), flying);

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

        if (_basicAttackRemainCoolTime < TimeSpan.Zero)
        {
            _basicAttackRemainCoolTime = TimeSpan.Zero;
        }

        for (int i = 0; i < GameConst.SkillCount; i++)
        {
            _skillRemainCoolTime[i] -= GameController.Instance.GetPlayerDeltaTime(Team);
            if (_skillRemainCoolTime[i] < TimeSpan.Zero)
            {
                _skillRemainCoolTime[i] = TimeSpan.Zero;
            }
        }
    }

    public int GetHitPoint()
    {
        return Mathf.CeilToInt(_hitPoint);
    }

    public float HitPointRatio()
    {
        return (float)_hitPoint / (float)_hitPointMax;
    }

    public int GetCoolTime(int attackIndex)
    {
        if (attackIndex == 0)
        {
            return (int) _basicAttackRemainCoolTime.TotalSeconds;
        }
        else
        {
            return (int) _skillCoolTime[attackIndex - 1].TotalSeconds;
        }
    }

    public float GetCoolTimeRatio(int attackIndex)
    {
        if (attackIndex == 0)
        {
            return (float) _basicAttackRemainCoolTime.TotalSeconds / (float) _basicAttackCoolTime.TotalSeconds;
        }
        else
        {
            return (float) _skillRemainCoolTime[attackIndex - 1].TotalSeconds / (float) _skillCoolTime[attackIndex - 1].TotalSeconds;
        }
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
        _hitPoint = Mathf.Clamp(_hitPoint + amount, 0, _hitPointMax);
    }

    public void AddSpecialPoint(float amount)
    {
        _specialPoint = Mathf.Clamp(_specialPoint + amount, 0, _speicalPointMax);
    }

    protected abstract void InitializeBaseStats();

    public virtual void OnPressDirection(Direction direction)
    {
        if (!IsMoveAble()) return;


        GraduallyMove(direction, GetMoveSpeed() * Time.deltaTime);

        //_rigidbody2D.MovePosition(transform.localPosition + (GetMoveSpeed() * Time.deltaTime * Utils.DirectionToVector3(direction)));

        Direction = direction;
        _spinnedTransform.localRotation = Utils.DirectionToQuaternion(direction);
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
        if (_state is CharacterState.ForcedMove) return false;

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
        if (_state is CharacterState.ForcedMove) return false;

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
        if (_state is CharacterState.ForcedMove) return false;

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
        return _moveSpeed * ratio;
    }

    public float GetBasicAttackCoolTimeRatio()
    {
        return (float)_basicAttackRemainCoolTime.TotalSeconds / (float)_basicAttackCoolTime.TotalSeconds;
    }

    public float GetSkillCoolTimeRatio(int skillIndex)
    {
        return (float)_skillRemainCoolTime[skillIndex].TotalSeconds / (float)_skillCoolTime[skillIndex].TotalSeconds;
    }

    public void ChangeStateIdle()
    {
        _state = new CharacterState.Idle();
        UpdateWallPassable(false);
        UpdateFlying(false);
    }

    public void ChangeStateDash(Direction direction, float power, TimeSpan dashTime, bool isWallPassable, bool isFlying)
    {
        _state = new CharacterState.Dash(
            direction, power,
            GameController.Instance.GetPlayerTime(Team), GameController.Instance.GetPlayerTime(Team) + dashTime);
        UpdateWallPassable(isWallPassable);
        UpdateFlying(isFlying);
    }

    public void ChangeStateRush(Direction direction, float power, bool isWallPassable, bool isFlying)
    {
        _state = new CharacterState.Rush(direction, power);
        UpdateWallPassable(isWallPassable);
        UpdateFlying(isFlying);
    }

    public void ChangeStateDrive(TimeSpan driveTime, bool isWallPassable, bool isFlying)
    {
        _state = new CharacterState.Drive(GameController.Instance.GetPlayerTime(Team), GameController.Instance.GetPlayerTime(Team) + driveTime);
        UpdateWallPassable(isWallPassable);
        UpdateFlying(isFlying);
    }

    public void ChangeStateForcedMove(Direction direction, float power, TimeSpan forcedMoveTime, bool isWallPassable, bool isFlying)
    {
        _state = new CharacterState.ForcedMove(direction, power,
            GameController.Instance.GetPlayerTime(Team), GameController.Instance.GetPlayerTime(Team) + forcedMoveTime);
        UpdateWallPassable(isWallPassable);
        UpdateFlying(isFlying);
    }




    private bool IsSomething(Direction direction)
    {
        int layerMask = LayerMask.GetMask("Wall");
        
        //RaycastHit2D hit = Physics2D.Raycast(transform.localPosition, Utils.DirectionToVector3(direction), 0.3f, layerMask);

        if ((direction == Direction.Up) || (direction == Direction.UpLeft) || (direction == Direction.UpRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Up);
            RaycastHit2D hitUp = Physics2D.Raycast(transform.localPosition, rayDirection, 0.6f, layerMask);
            
            // Raycast 시각화 (빨간색: 충돌 없음, 초록색: 충돌 있음)
            Color rayColor = hitUp.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.6f, rayColor, 0.1f);
            
            if (hitUp.collider != null) return true;
        }

        if ((direction == Direction.Down) || (direction == Direction.DownLeft) || (direction == Direction.DownRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Down);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.localPosition, rayDirection, 0.6f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitDown.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.6f, rayColor, 0.1f);
            
            if (hitDown.collider != null) return true;
        }

        if ((direction == Direction.Left) || (direction == Direction.UpLeft) || (direction == Direction.DownLeft))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Left);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.localPosition, rayDirection, 0.4f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitLeft.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.4f, rayColor, 0.1f);
            
            if (hitLeft.collider != null) return true;
        }
        
        if ((direction == Direction.Right) || (direction == Direction.UpRight) || (direction == Direction.DownRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Right);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.localPosition, rayDirection, 0.4f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitRight.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.4f, rayColor, 0.1f);
            
            if (hitRight.collider != null) return true;
        }

        return false;
    }

    private void GraduallyMove(Direction direction, float power)
    {
        float step = 0.01f;

        if (!_isWallPassable && IsSomething(direction))
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

    public Transform GetSpinnedTransform()
    {
        return _spinnedTransform;
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
    public sealed record Dash(Direction direction, float power, TimeSpan startTime, TimeSpan endTime) : CharacterState;
    public sealed record Rush(Direction direction, float power) : CharacterState;
    public sealed record Drive(TimeSpan startTime, TimeSpan endTime) : CharacterState;
    public sealed record ForcedMove(Direction direction, float power, TimeSpan startTime, TimeSpan endTime) : CharacterState;

}
