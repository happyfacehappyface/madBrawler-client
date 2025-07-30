using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterJaehyeon : Character
{

    private List<Controllable> _computers;

    private const float _skill0BuffDuration = 0.2f;
    private const float _skill0BuffFactor = 1.2f;

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

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (GetComputerMinDistance() <= 1.0f)
        {
            AddEffect(GameController.Instance.CharacterEffectFactory.JaehyeonSkill0BuffMoveSpeed(TimeSpan.FromSeconds(_skill0BuffDuration), _skill0BuffFactor));
        }
    }


    public override void OnPressDirection(Direction direction)
    {
        base.OnPressDirection(direction);
    }

    
    public override bool OnPressBasicAttack()
    {
        if (!base.OnPressBasicAttack()) return false;
        GameController.Instance.ProjectileHandler.CreateJaehyeonBasicAttack(Team, GetDirection());
        SoundManager.Instance.PlayVoiceJaehyeonBasicAttack(0.0f);
        return true;
    }

    public override bool IsSkill0Able()
    {
        return base.IsSkill0Able() && GetComputerMinDistance() > 1.0f;
    }

    public override bool IsSkill2Able()
    {
        return base.IsSkill2Able() && _computers.Count >= 2;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;

        Controllable computer = GameController.Instance.ControllableHandler.CreateJaehyeonSkill0(Team);
        _computers.Add(computer);
        SoundManager.Instance.PlayVoiceJaehyeonSkill0(0.0f);
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

        SoundManager.Instance.PlayVoiceJaehyeonSkill1(0.0f);

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

        SoundManager.Instance.PlayVoiceJaehyeonSkill2(0.0f);
        
        return true;
    }

    private float GetComputerMinDistance()
    {
        float minDistance = float.MaxValue;
        foreach (Controllable computer in _computers)
        {
            float distance = Vector2.Distance(transform.position, computer.GetPosition());
            minDistance = Mathf.Min(minDistance, distance);
        }
        return minDistance;
    }

    
    public override string GetCharacterName()
    {
        return "재현";
    }

    public override string GetBasicAttackName()
    {
        return "손가락 튕기기";
    }


    public override string GetSkillName(int skillIndex)
    {
        if (skillIndex == 0)
        {
            return "컴퓨터";
        }
        else if (skillIndex == 1)
        {
            return "P2P";
        }
        else
        {
            return "블록 체인";
        }
    }

    public override string GetSpecialPointName()
    {
        return "";
    }
}
