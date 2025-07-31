using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGwangho : Character
{
    private bool _isSkill0Controlling = false;
    private Controllable _skill0Controllable = null;

    private float _skill0Ratio;

    private StockState _stockState;

    private TimeSpan _lastSpecialPointUpdateTime;


    protected override void InitializeBaseStats()
    {
        _hitPoint = 85f;
        _hitPointMax = 85f;
        _moveSpeed = 4.4f;
        
        _specialPoint = 30f;
        _speicalPointMax = 100f;

        _basicAttackCoolTime = TimeSpan.FromSeconds(0.5f);
        _skillCoolTime = new TimeSpan[GameConst.SkillCount]
        {
            TimeSpan.FromSeconds(9.0f),
            TimeSpan.FromSeconds(3.0f),
            TimeSpan.FromSeconds(30.0f)
        };

        _skillRemainCoolTime = new TimeSpan[GameConst.SkillCount]
        {
            TimeSpan.FromSeconds(0.0f),
            TimeSpan.FromSeconds(3.0f),
            TimeSpan.FromSeconds(20.0f)
        };

        _stockState = new StockState.Random(-10, 10, GameController.Instance.GetPlayerTime(Team) + TimeSpan.FromSeconds(5.0f));
        _lastSpecialPointUpdateTime = GameController.Instance.GetPlayerTime(Team);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        UpdateStockState();
        UpdateSpecialPointByStockState();
    }


    private void UpdateStockState()
    {
        switch (_stockState)
        {
            case StockState.Random random:
                if (GameController.Instance.GetPlayerTime(Team) >= random.EndTime)
                {
                    _stockState = GetNewRandomStockState();
                }
                break;
            case StockState.Fixed fix:
                if (GameController.Instance.GetPlayerTime(Team) >= fix.EndTime)
                {
                    _stockState = GetNewRandomStockState();
                }
                break;
        }
    }

    private StockState GetNewRandomStockState()
    {
        int thresholdLow = 15;
        int thresholdMedium = 35;
        int thresholdHigh = 50;
        int thresholdVeryHigh = 75;

        TimeSpan randomEndTime = GameController.Instance.GetPlayerTime(Team) + TimeSpan.FromSeconds(UnityEngine.Random.Range(5.0f, 15.0f));

        if (GetSpecialPoint() <= thresholdLow)
        {
            return new StockState.Random(-5, 20, randomEndTime);
        }
        else if (GetSpecialPoint() <= thresholdMedium)
        {
            return new StockState.Random(-10, 15, randomEndTime);
        }
        else if (GetSpecialPoint() <= thresholdHigh)
        {
            return new StockState.Random(-15, 10, randomEndTime);
        }
        else if (GetSpecialPoint() <= thresholdVeryHigh)
        {
            return new StockState.Random(-20, 5, randomEndTime);
        }
        else
        {
            return new StockState.Random(-25, 2, randomEndTime);
        }

    }

    private void UpdateSpecialPointByStockState()
    {
        if (GameController.Instance.GetPlayerTime(Team) - _lastSpecialPointUpdateTime > TimeSpan.FromSeconds(1.0f))
        {
            _lastSpecialPointUpdateTime = GameController.Instance.GetPlayerTime(Team);

            switch (_stockState)
            {
                case StockState.Random random:
                    AddSpecialPoint(UnityEngine.Random.Range(random.RangeMin, random.RangeMax));
                    break;
                case StockState.Fixed fix:
                    _specialPoint = fix.Value;
                    break;
            }
        }
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

        float lifeTime = 1.0f + SpecialPointRatio() * 3.0f;


        if (GetSpecialPoint() >= 100f)
        {
            GameController.Instance.ProjectileHandler.CreateGwanghoBasicAttackStrong(Team, GetDirection(), lifeTime);
            SoundManager.Instance.PlayVoiceGwanghoBasicAttack(0.0f);
        }
        else
        {
            GameController.Instance.ProjectileHandler.CreateGwanghoBasicAttack(Team, GetDirection(), lifeTime);
        }
        
        return true;
    }

    public override bool OnPressSkill0()
    {
        if (!base.OnPressSkill0()) return false;

        if (_isSkill0Controlling)
        {
            _isSkill0Controlling = false;

            float scale = 0.5f + (_skill0Ratio * 3.0f);
            bool isStun = _skill0Ratio >= 1.0f;

            GameController.Instance.ProjectileHandler.CreateGwanghoSkill0After(Team, _skill0Controllable.GetPosition(), scale, isStun);
            Destroy(_skill0Controllable.gameObject);
            _skill0Controllable = null;
            
        }
        else
        {
            _isSkill0Controlling = true;

            _skill0Ratio = SpecialPointRatio();
            float skill0Scale = 0.5f + (_skill0Ratio * 3.0f);
            
            _skill0Controllable = GameController.Instance.ControllableHandler.CreateGwanghoSkill0(Team, skill0Scale);
            ResetSkillCoolTime(0);
        }

        return true;
    }

    public override bool OnPressSkill1()
    {
        if (!base.OnPressSkill1()) return false;

        float distance = 10f;
        float power = 10f + (SpecialPointRatio() * 24.0f);
        float lifeTime = distance / power;

        ChangeStateDash(GetDirection(), power, TimeSpan.FromSeconds(lifeTime), true, false);
        GameController.Instance.ProjectileHandler.CreateGwanghoSkill1(Team, GetDirection(), lifeTime);
        SoundManager.Instance.PlayVoiceGwanghoSkill1(0.0f);
        return true;
    }

    public override bool OnPressSkill2()
    {
        if (!base.OnPressSkill2()) return false;

        _stockState = new StockState.Fixed((int)_speicalPointMax, GameController.Instance.GetPlayerTime(Team) + TimeSpan.FromSeconds(10.0f));

        SoundManager.Instance.PlayVoiceGwanghoSkill2(0.0f);
        SoundManager.Instance.PlaySfxStock(0.0f);

        return true;
    }




    public override string GetSpecialPointName()
    {
        return "주식";
    }



    private abstract record StockState
    {
        public sealed record Random(int RangeMin, int RangeMax, TimeSpan EndTime) : StockState;
        public sealed record Fixed(int Value, TimeSpan EndTime) : StockState;
    }

}








