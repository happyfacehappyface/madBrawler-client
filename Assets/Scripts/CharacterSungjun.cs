using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSungjun : Character
{
    protected override void InitializeBaseStats()
    {
        HitPoint = 100f;
        HitPointMax = 100f;
        MoveSpeed = 4f;
        
        SpecialPoint = 0f;
        SpecialPointMax = 100f;

        _basicAttackCoolTime = TimeSpan.FromSeconds(0.5f);
        _skillCoolTime = new TimeSpan[GameConst.SkillCount] 
        {
            TimeSpan.FromSeconds(2.0f),
            TimeSpan.FromSeconds(2.0f),
            TimeSpan.FromSeconds(2.0f)
        };
    }


    public override void OnPressDirection(Direction direction)
    {
        base.OnPressDirection(direction);
    }

    
    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        Debug.Log("Sungjun OnPressBasicAttack");
        GameController.Instance.ProjectileHandler.CreateSungjunProjectile(GetPosition(), Utils.DirectionToVector2(GetDirection()), 10, Team);
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        Debug.Log("Sungjun OnPressSkill0");
        AddEffect(new CharacterEffect(Team, TimeSpan.FromSeconds(3), CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(3), "MoveSpeed", null));
        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;
        Debug.Log("Sungjun OnPressSkill1");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill2(Team);
        ChangeStateDrive(false, TimeSpan.FromSeconds(2.0f));
        AddEffect(new CharacterEffect(Team, TimeSpan.FromSeconds(2.0f), CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(2.0f), "MoveSpeed", null));
        return true;
    }


}
