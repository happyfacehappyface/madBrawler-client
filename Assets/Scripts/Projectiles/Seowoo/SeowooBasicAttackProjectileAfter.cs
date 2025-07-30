using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SeowooBasicAttackProjectileAfter : BasicProjectile
{
    private const float _lifeTime = 3.0f;
    private const float _damage = 1f;

    private const float _lastTime = 1.5f;

    [SerializeField] private Animator _animator;

    

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, 0f, _damage);

        HitIntervalTime = TimeSpan.FromSeconds(0.1f);
        SoundManager.Instance.PlaySfxFireballGround(0.0f);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (_timeFromStart > TimeSpan.FromSeconds(_lastTime))
        {
            _animator.SetTrigger("End");
        }
    }

    protected override bool IsHarmful()
    {
        return _timeFromStart < TimeSpan.FromSeconds(_lastTime);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        
        return true;
    }

    

}
