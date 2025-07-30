using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterSeowoo : Character
{

    private const float _skill0SpecialCost = 10f;
    private const float _skill1SpecialCost = 0f;
    private const float _skill2SpecialCost = 20f;

    private const float _skill1BuffDuration = 0.6f;
    private const float _skill1BuffFactor = 1.3f;

    private const float _skill2DebuffDuration = 1.0f;


    private bool _isPortalExist = false;
    private Controllable _portalControllable = null;
    
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

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        AddSpecialPoint((float)GameController.Instance.GetPlayerDeltaTime(Team).TotalSeconds * 3f);
    }

    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        GameController.Instance.ProjectileHandler.CreateSeowooBasicAttack(Team, GetDirection());
        SoundManager.Instance.PlayVoiceSeowooBasicAttack(0.0f);
        
        return true;
    }

    protected override bool IsSkill0Able()
    {
        return base.IsSkill0Able() && _specialPoint >= _skill0SpecialCost;
    }

    protected override bool IsSkill1Able()
    {
        return base.IsSkill1Able() && _specialPoint >= _skill1SpecialCost;
    }

    protected override bool IsSkill2Able()
    {
        return base.IsSkill2Able() && _specialPoint >= _skill2SpecialCost;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;
        GameController.Instance.ProjectileHandler.CreateSeowooSkill0(Team, GetDirection());
        AddSpecialPoint((-1) * _skill0SpecialCost);
        SoundManager.Instance.PlayVoiceSeowooSkill0(0.0f);
        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;

        if (_isPortalExist)
        {
            transform.localPosition = _portalControllable.GetPosition();
            Destroy(_portalControllable.gameObject);
            _portalControllable = null;
            _isPortalExist = false;

            AddEffect(GameController.Instance.CharacterEffectFactory.SeowooSkill1BuffMoveSpeed(TimeSpan.FromSeconds(_skill1BuffDuration), _skill1BuffFactor));
        
            SoundManager.Instance.PlaySfxPortalRide(0.0f);
        }
        else
        {
            _portalControllable = GameController.Instance.ControllableHandler.CreateSeowooSkill1(Team);
            _isPortalExist = true;
            ResetSkillCoolTime(1);
            SoundManager.Instance.PlaySfxPortalOn(0.0f);
        }

        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;
        GameController.Instance.ProjectileHandler.CreateSeowooSkill2(Team, GetDirection());
        AddSpecialPoint((-1) * _skill2SpecialCost);
        AddEffect(GameController.Instance.CharacterEffectFactory.SeowooSkill2DebuffStun(TimeSpan.FromSeconds(_skill2DebuffDuration)));
        SoundManager.Instance.PlayVoiceSeowooSkill2(0.0f);
        return true;
    }



}
