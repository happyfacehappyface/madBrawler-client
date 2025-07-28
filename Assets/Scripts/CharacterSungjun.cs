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

        _basicAttackCoolTime = TimeSpan.FromSeconds(0.2f);
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
        GameController.Instance.ProjectileHandler.CreateSungjunBasicAttack(Team, GetDirection());
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        Debug.Log("Sungjun OnPressSkill0");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill0(Team, 2.0f);
        ChangeStateDrive(false, TimeSpan.FromSeconds(2.0f));
        AddEffect(new CharacterEffect(Team, TimeSpan.FromSeconds(2.0f), CharacterEffectType.Buff, new CharacterEffectCategory.MoveSpeed(2.0f), "MoveSpeed", null));
        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;
        Debug.Log("Sungjun OnPressSkill1");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill1(Team, GetDirection());
        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;
        Debug.Log("Sungjun OnPressSkill2");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill1(Team, GetDirection());
        return true;
    }


}
