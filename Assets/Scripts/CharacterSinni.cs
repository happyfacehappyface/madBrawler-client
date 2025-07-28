using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSinni : Character
{
    protected override void InitializeBaseStats()
    {
        _hitPoint = 100f;
        _hitPointMax = 100f;
        _moveSpeed = 6f;
        
        _specialPoint = 50f;
        _speicalPointMax = 100f;

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
        Debug.Log("Sinni OnPressBasicAttack");
        GameController.Instance.ProjectileHandler.CreateSinniBasicAttack(Team, GetDirection());
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        Debug.Log("Sungjun OnPressSkill0");
        GameController.Instance.ProjectileHandler.CreateSungjunSkill0(Team, 2.0f);
        ChangeStateDrive(TimeSpan.FromSeconds(2.0f), true, false);
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
        float bounceTime = 0.8f;
        GameController.Instance.ProjectileHandler.CreateSungjunSkill2(Team, GetDirection(), bounceTime);
        GameController.Instance.ProjectileHandler.CreateSungjunSkill2After(Team, GetDirection(), bounceTime);
        ChangeStateForcedMove(GetDirection(), 0, TimeSpan.FromSeconds(bounceTime), true, true);
        return true;
    }


}
