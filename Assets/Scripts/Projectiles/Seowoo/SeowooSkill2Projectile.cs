using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeowooSkill2Projectile : BasicProjectile
{

    [SerializeField] private Animator _animator;
    private const float _lifeTime = 6.0f;

    private const float _delayTime = 1.2f;
    private const float _speed = 0.01f;
    private const float _damage = 65f;

    private const float _stunDuration = 2.0f;

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

        if (_timeFromStart > TimeSpan.FromSeconds(_delayTime))
        {
            _animator.SetTrigger("Thunder");
        }
        
        if (_timeFromStart > TimeSpan.FromSeconds(_delayTime + 1.0f))
        {
            _animator.SetTrigger("End");
        }
    }

    protected override bool IsHarmful()
    {
        return base.IsHarmful() &&
        (_timeFromStart > TimeSpan.FromSeconds(_delayTime)) &&
        _timeFromStart < TimeSpan.FromSeconds(_delayTime + 1.0f);
    }

    private TimeSpan GetTimeLeft()
    {
        if (_timeFromStart > TimeSpan.FromSeconds(_delayTime + 1.0f))
        {
            return TimeSpan.Zero;
        }
        else
        {
            return TimeSpan.FromSeconds(_delayTime + 1.0f) - _timeFromStart;
        }
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        character.AddEffect(GameController.Instance.CharacterEffectFactory.SeowooSkill2DebuffStun(GetTimeLeft()));

        return true;
    }
}
