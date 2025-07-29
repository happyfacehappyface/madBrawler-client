using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterJaehyeon : Character
{

    private List<Controllable> _computers;

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

        _computers = new List<Controllable>();
    }


    public override void OnPressDirection(Direction direction)
    {
        base.OnPressDirection(direction);
    }

    
    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        GameController.Instance.ProjectileHandler.CreateJaehyeonBasicAttack(Team, GetDirection());
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;

        Controllable computer = GameController.Instance.ControllableHandler.CreateJaehyeonSkill0(Team);
        _computers.Add(computer);

        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;

        Vector2 destination = GameController.Instance.GetPlayerTransform(Utils.GetOppositeTeam(Team)).position;
        Vector2 myPosition = transform.position;
        float angle2 = Mathf.Atan2(destination.y - myPosition.y, destination.x - myPosition.x) * Mathf.Rad2Deg;

        GameController.Instance.ProjectileHandler.CreateJaehyeonSkill1(Team, angle2, myPosition);

        foreach (Controllable computer in _computers)
        {
            Vector2 origin = computer.GetPosition();
            
            float angle = Mathf.Atan2(destination.y - origin.y, destination.x - origin.x) * Mathf.Rad2Deg;

            GameController.Instance.ProjectileHandler.CreateJaehyeonSkill1(Team, angle, origin);

        }

        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;

        int computerCount = _computers.Count;
        for (int i = 0; i < computerCount; i++)
        {
            for (int j = i + 1; j < computerCount; j++)
            {
                Controllable computer1 = _computers[i];
                Controllable computer2 = _computers[j];
                Vector2 origin = computer1.GetPosition();
                Vector2 destination = computer2.GetPosition();

                GameController.Instance.ProjectileHandler.CreateJaehyeonSkill2(Team, origin, destination);
            }
        }
        
        return true;
    }
}
