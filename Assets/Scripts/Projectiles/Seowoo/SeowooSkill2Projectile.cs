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

    private const float _stunDuration = 1.0f;

    private bool _isThunderSoundPlayed = false;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, _speed, _damage);

        SoundManager.Instance.PlaySfxStorm(0.0f);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (!_isThunderSoundPlayed && _timeFromStart > TimeSpan.FromSeconds(_delayTime))
        {
            SoundManager.Instance.PlaySfxThunder(0.0f);
            _isThunderSoundPlayed = true;
        }
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
