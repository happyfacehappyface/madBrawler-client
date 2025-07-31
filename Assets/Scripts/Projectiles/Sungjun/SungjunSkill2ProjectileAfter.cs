using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill2ProjectileAfter : BasicProjectile
{
    private const float _damage = 25f;
    private float _delayTime;
    private const float _slowDuration = 1.0f;
    private const float _slowFactor = 0.5f;

    [SerializeField] private Animator _animator;

    public void Initialize(float delayTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            delayTime + 3.0f, 0.0f, _damage);
        _delayTime = delayTime;
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();
        
        if (_timeFromStart > TimeSpan.FromSeconds(_delayTime))
        {
            _animator.SetTrigger("Crack");
        }

        if (_timeFromStart > TimeSpan.FromSeconds(_delayTime + 2.5f))
        {
            _animator.SetTrigger("Remove");
        }
    }

    protected override bool IsHarmful()
    {
        return _timeFromStart > TimeSpan.FromSeconds(_delayTime) && _timeFromStart < TimeSpan.FromSeconds(_delayTime + 0.2f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        character.AddEffect(GameController.Instance.CharacterEffectFactory.SungjunSkill2DebuffMoveSpeed(TimeSpan.FromSeconds(_slowDuration), _slowFactor));
        SoundManager.Instance.PlayVoiceSungjunSkill2After(0.0f);
        SoundManager.Instance.PlaySfxFall(0.0f);
        return true;
    }
}
