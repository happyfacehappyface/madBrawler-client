using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGwangho : Character
{
    private bool _isSkill0Controlling = false;
    private Controllable _skill0Controllable = null;


    protected override void InitializeBaseStats()
    {
        _hitPoint = 100f;
        _hitPointMax = 100f;
        _moveSpeed = 5f;
        
        _specialPoint = 0f;
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
        if (_isSkill0Controlling)
        {
            _skill0Controllable.OnPressDirection(direction);
        }
        else
        {
            base.OnPressDirection(direction);
        }
    }

    
    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        GameController.Instance.ProjectileHandler.CreateGwanghoBasicAttack(Team, GetDirection());
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;

        if (_isSkill0Controlling)
        {
            _isSkill0Controlling = false;
            
            GameController.Instance.ProjectileHandler.CreateGwanghoSkill0After(Team, _skill0Controllable.GetPosition(), 1.0f);
            Destroy(_skill0Controllable.gameObject);
            _skill0Controllable = null;
            
        }
        else
        {
            _isSkill0Controlling = true;
            _skill0Controllable = GameController.Instance.ControllableHandler.CreateGwanghoSkill0(Team);
            ResetSkillCoolTime(0);
        }

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
