using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniSkill0Projectile : BasicProjectile
{
    private const float _lifeTime = 5.0f;
    private const float _damage = 2f;

    private bool _isSilenceActivated;
    

    public void Initialize(Direction direction, float specialRatio)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, 0f, _damage);

        float scaleRatio = Mathf.Clamp(specialRatio, 0f, 0.75f);
        float scale = 0.3f + scaleRatio * 2.7f;
        _isSilenceActivated = specialRatio >= 1.0f;

        transform.localScale = new Vector3(scale, scale, scale);

        HitIntervalTime = TimeSpan.FromSeconds(0.1f);
        SoundManager.Instance.PlaySfxClap(0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        if (_isSilenceActivated)
        {
            character.AddEffect(GameController.Instance.CharacterEffectFactory.SinniSkill0DebuffSilence(TimeSpan.FromSeconds(0.2f)));
        }
        
        return true;
    }

    

}
