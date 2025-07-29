using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill1Projectile : BasicProjectile
{


    private const float _lifeTime = 2.0f;
    private const float _speed = 12f;
    private const float _damage = 10f;


    private const float _hookPower = 10f;
    private const float _hookTime = 0.5f;
    private const float _slowDuration = 1.0f;
    private const float _slowFactor = 0.5f;
    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, true,
            _lifeTime, _speed, _damage);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        character.ChangeStateForcedMove(_direction, (-1) *_hookPower, TimeSpan.FromSeconds(_hookTime), true, false);
        character.AddEffect(GameController.Instance.CharacterEffectFactory.SungjunSkill1DebuffMoveSpeed(TimeSpan.FromSeconds(_slowDuration), _slowFactor));
        
        return true;
    }
}
