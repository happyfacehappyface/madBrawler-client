using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSinni : Character
{
    protected override void InitializeBaseStats()
    {
        _hitPoint = 105f;
        _hitPointMax = 105f;
        _moveSpeed = 6f;
        
        _specialPoint = 30f;
        _speicalPointMax = 100f;

        _basicAttackCoolTime = TimeSpan.FromSeconds(0.4f);
        _skillCoolTime = new TimeSpan[GameConst.SkillCount] 
        {
            TimeSpan.FromSeconds(7.0f),
            TimeSpan.FromSeconds(5.0f),
            TimeSpan.FromSeconds(26.0f)
        };

        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount]
        {
            TimeSpan.FromSeconds(0.0f),
            TimeSpan.FromSeconds(8.0f),
            TimeSpan.FromSeconds(25.0f)
        };
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        AddSpecialPoint((float)GameController.Instance.GetPlayerDeltaTime(Team).TotalSeconds * 5f);
    }

    
    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        GameController.Instance.ProjectileHandler.CreateSinniBasicAttack(Team, GetDirection());
        SoundManager.Instance.PlayVoiceSinniBasicAttack(0.0f);
        return true;
    }

    public override bool IsSkill0Able()
    {
        return base.IsSkill0Able() && _specialPoint >= 10f;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        GameController.Instance.ProjectileHandler.CreateSinniSkill0(Team, Direction.Right, SpecialPointRatio());
        AddSpecialPoint((-1) * _specialPoint);
        SoundManager.Instance.PlayVoiceSinniSkill0(0.0f);
        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;

        ChangeStateDash(GetDirection(), 4f, TimeSpan.FromSeconds(0.3f), true, true);
        AddEffect(GameController.Instance.CharacterEffectFactory.SinniSkill1BuffMoveSpeed(TimeSpan.FromSeconds(0.6f), 1.3f));
        SoundManager.Instance.PlayVoiceSinniSkill1(0.0f);
        SoundManager.Instance.PlaySfxJump(0.0f);
        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;
        GameController.Instance.ProjectileHandler.CreateSinniSkill2(Team, GetDirection());
        SoundManager.Instance.PlayVoiceSinniSkill2(0.0f);
        return true;
    }

    

    public override string GetSpecialPointName()
    {
        return "노래방";
    }


}
