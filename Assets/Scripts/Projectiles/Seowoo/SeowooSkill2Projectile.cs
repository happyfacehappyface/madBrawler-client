using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeowooSkill2Projectile : BasicProjectile
{
    private const float _lifeTime = 3.0f;

    private const float _delayTime = 1.0f;
    private const float _speed = 0.01f;
    private const float _damage = 45f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, _speed, _damage);
    }

    protected override bool IsHarmful()
    {
        return base.IsHarmful() && (_timeFromStart > TimeSpan.FromSeconds(_delayTime));
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        return true;
    }
}
