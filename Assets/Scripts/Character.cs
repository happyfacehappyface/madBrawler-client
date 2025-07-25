using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour
{
    protected Direction Direction;
    protected Team Team;

    protected float HitPoint;
    protected float HitPointMax;
    protected float MoveSpeed;
    protected float SpecialPoint;
    protected float SpecialPointMax;

    protected List<CharacterEffect> _effects = new List<CharacterEffect>();

    protected TimeSpan _basicAttackCoolTime;
    protected TimeSpan[] _skillCoolTime;
    protected TimeSpan _basicAttackRemainCoolTime;
    protected TimeSpan[] _skillRemainCoolTime;


    public virtual void ManualStart(Team team)
    {
        InitializeBaseStats();
        Team = team;
        _effects = new List<CharacterEffect>();

        _basicAttackRemainCoolTime = TimeSpan.Zero;
        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount];
    }

    public virtual void ManualUpdate()
    {
        CleanEffects();
        UpdateCoolTime();
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


    protected void AddHitPoint(int amount)
    {
        HitPoint = Mathf.Clamp(HitPoint + amount, 0, HitPointMax);
    }

    protected void AddSpecialPoint(int amount)
    {
        SpecialPoint = Mathf.Clamp(SpecialPoint + amount, 0, SpecialPointMax);
    }

    protected abstract void InitializeBaseStats();

    public virtual void OnPressUp()
    {
        if (!IsMoveAble()) return;
        transform.localPosition += Time.deltaTime * GetMoveSpeed() * new Vector3(0, 1, 0);
        Direction = Direction.Up;
    }

    public virtual void OnPressDown()
    {
        if (!IsMoveAble()) return;
        transform.localPosition += Time.deltaTime * GetMoveSpeed() * new Vector3(0, -1, 0);
        Direction = Direction.Down;
    }

    public virtual void OnPressLeft()
    {
        if (!IsMoveAble()) return;
        transform.localPosition += Time.deltaTime * GetMoveSpeed() * new Vector3(-1, 0, 0);
        Direction = Direction.Left;
    }
    
    public virtual void OnPressRight()
    {
        if (!IsMoveAble()) return;
        transform.localPosition += Time.deltaTime * GetMoveSpeed() * new Vector3(1, 0, 0);
        Direction = Direction.Right;
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

        if (projectile.Team == Team)
        {
            return;
        }

        AddHitPoint((-1) * projectile.Damage);
        Debug.Log("OnHitByProjectile");
    }
    

    private bool IsMoveAble()
    {
        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.Stun) return false;
            if (effect.EffectCategory is CharacterEffectCategory.Bond) return false;
        }
        return true;
    }

    private bool IsBasicAttackAble()
    {
        if (_basicAttackRemainCoolTime > TimeSpan.Zero) return false;

        foreach (var effect in _effects)
        {
            if (effect.EffectCategory is CharacterEffectCategory.Stun) return false;
        }
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
    
    


}


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
