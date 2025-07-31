using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSungjun : Character
{
    protected override void InitializeBaseStats()
    {
        _hitPoint = 120f;
        _hitPointMax = 120f;
        _moveSpeed = 4.8f;
        
        _specialPoint = 0f;
        _speicalPointMax = 100f;

        _basicAttackCoolTime = TimeSpan.FromSeconds(0.3f);
        _skillCoolTime = new TimeSpan[GameConst.SkillCount] 
        {
            TimeSpan.FromSeconds(5.0f),
            TimeSpan.FromSeconds(6.0f),
            TimeSpan.FromSeconds(12.0f)
        };

        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount]
        {
            TimeSpan.FromSeconds(0.0f),
            TimeSpan.FromSeconds(12.0f),
            TimeSpan.FromSeconds(21.0f)
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
        SoundManager.Instance.PlayVoiceSungjunBasicAttack(0.0f);
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        Debug.Log("Sungjun OnPressSkill0");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill0(Team, 1.6f);
        ChangeStateDrive(TimeSpan.FromSeconds(1.6f), true, false);
        AddEffect(GameController.Instance.CharacterEffectFactory.SungjunSkill0BuffMoveSpeed(TimeSpan.FromSeconds(1.6f), 1.5f));
        SoundManager.Instance.PlayVoiceSungjunSkill0(0.0f);
        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;
        Debug.Log("Sungjun OnPressSkill1");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill1(Team, GetDirection());
        SoundManager.Instance.PlayVoiceSungjunSkill1(0.0f);
        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;
        Debug.Log("Sungjun OnPressSkill2");
        float bounceTime = 0.8f;
        GameController.Instance.ProjectileHandler.CreateSungjunSkill2(Team, GetDirection(), bounceTime);
        GameController.Instance.ProjectileHandler.CreateSungjunSkill2After(Team, GetDirection(), bounceTime);
        ChangeStateForcedMove(GetDirection(), 0, TimeSpan.FromSeconds(bounceTime), true, true);
        SoundManager.Instance.PlayVoiceSungjunSkill2(0.0f);
        return true;
    }

    

    public override string GetSpecialPointName()
    {
        return "";
    }


}
