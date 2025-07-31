using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GwanghoSkill0ProjectileAfter : BasicProjectile
{
    private const float _lifeTime = 0.5f;
    private const float _damage = 16f;

    private const float _delayTime = 0.15f;
    private const float _lastTime = 0.5f;

    private bool _isStun;

    [SerializeField] private Animator _animator;

    public void Initialize(float scale, bool isStun)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            _lifeTime, 0f, _damage);

        transform.localScale = new Vector3(scale, scale, scale);

        _isStun = isStun;

        SoundManager.Instance.PlaySfxDinosaurStomp(0.0f);
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
        return _timeFromStart < TimeSpan.FromSeconds(_lastTime) && _timeFromStart > TimeSpan.FromSeconds(_delayTime);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        if (_isStun)
        {
            character.AddEffect(GameController.Instance.CharacterEffectFactory.GwangHoSkill1DebuffStun(TimeSpan.FromSeconds(1.0f)));
        }

        return true;
    }
}
