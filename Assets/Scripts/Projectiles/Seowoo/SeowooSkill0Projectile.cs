using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeowooSkill0Projectile : BasicProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 46f;
    private const float _damage = 3f;
    private const float _stunDuration = 1.0f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, true, true,
            _lifeTime, _speed, _damage);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        character.AddEffect(GameController.Instance.CharacterEffectFactory.SeowooSkill0DebuffStun(TimeSpan.FromSeconds(_stunDuration)));

        return true;
    }
}
