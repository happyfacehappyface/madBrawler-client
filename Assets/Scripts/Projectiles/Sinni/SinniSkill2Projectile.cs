using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniSkill2Projectile : BasicProjectile
{
    [SerializeField] private Animator _animator;

    private const float _lastTime = 0.7f;
    private const float _delayTime = 0.3f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            1.5f, 0.0f, 0.0f);
    }


    protected override bool IsHarmful()
    {
        return base.IsHarmful() &&
        _timeFromStart < TimeSpan.FromSeconds(_lastTime) &&
        _timeFromStart > TimeSpan.FromSeconds(_delayTime);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (_timeFromStart > TimeSpan.FromSeconds(_lastTime))
        {
            _animator.SetTrigger("End");
        }
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        
        //character.ChangeStateForcedMove(_direction, 0, TimeSpan.FromSeconds(_bounceTime), true, true);

        _owner.transform.position = character.GetPosition();

        GameController.Instance.ProjectileHandler.CreateSinniSkill2After(Team, _direction);
        _owner.AddSpecialPoint(100f);
        
        return true;
    }
}
