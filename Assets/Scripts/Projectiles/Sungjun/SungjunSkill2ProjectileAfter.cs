using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill2ProjectileAfter : BasicProjectile
{
    private const float _damage = 30f;
    private float _delayTime;
    private const float _slowDuration = 1.0f;
    private const float _slowFactor = 0.5f;
    public void Initialize(float delayTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            delayTime + 0.2f, 0.0f, _damage);
        _delayTime = delayTime;
    }

    protected override bool IsHarmful()
    {
        return _timeFromStart > TimeSpan.FromSeconds(_delayTime);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        character.AddEffect(GameController.Instance.CharacterEffectFactory.SungjunSkill2DebuffMoveSpeed(TimeSpan.FromSeconds(_slowDuration), _slowFactor));
        return true;
    }
}
