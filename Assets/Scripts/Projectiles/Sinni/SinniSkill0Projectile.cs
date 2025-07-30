using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniSkill0Projectile : BasicProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _damage = 2f;

    private const float _lastTime = 1.5f;

    private bool _isSilenceActivated;

    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _particle;

    [SerializeField] private SpriteRenderer _radiusSprite;
    

    public void Initialize(Direction direction, float specialRatio)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, 0f, _damage);

        float scaleRatio = Mathf.Clamp(specialRatio, 0f, 0.75f);
        float scale = 0.3f + scaleRatio * 2.7f;
        _isSilenceActivated = specialRatio >= 1.0f;

        transform.localScale = new Vector3(scale, scale, scale);
        _particle.transform.localScale = new Vector3(scale, scale, scale);

        _radiusSprite.color = _isSilenceActivated ? new Color(182f/255f, 0f/255f, 224f/255f, 165f/255f) : new Color(189f/255f, 154f/255f, 197f/255f, 130f/255f);

        HitIntervalTime = TimeSpan.FromSeconds(0.1f);
        SoundManager.Instance.PlaySfxClap(0.0f);
    }


    protected override bool IsHarmful()
    {
        return _timeFromStart < TimeSpan.FromSeconds(_lastTime);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (_timeFromStart > TimeSpan.FromSeconds(_lastTime))
        {
            _animator.SetTrigger("End");
        }
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        if (_isSilenceActivated)
        {
            character.AddEffect(GameController.Instance.CharacterEffectFactory.SinniSkill0DebuffSilence(TimeSpan.FromSeconds(0.3f)));
        }
        
        return true;
    }

    

}
