using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JaehyeonBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 5.0f;
    private const float _speed = 1f;
    private const float _speedAcceleration = 30f;
    private const float _damage = 6f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, true, true,
            _lifeTime, _speed, _damage);

    }


    public override void ManualUpdate()
    {
        base.ManualUpdate();

        _currentSpeed += _speedAcceleration * Time.deltaTime;
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxWaveHit(0.0f);
        character.ChangeStateForcedMove(GetDirection(), 15f, TimeSpan.FromSeconds(0.1f), false, false);
        
        return true;
    }
}
