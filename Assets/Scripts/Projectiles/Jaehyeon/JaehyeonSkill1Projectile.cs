using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaehyeonSkill1Projectile : RotatingProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 18f;
    private const float _damage = 4f;

    private const float _bondAdditionalDamage = 3f;

    public void Initialize(float angle)
    {
        base.Initialize(
            angle, false, false, true,
            _lifeTime, _speed, _damage);

        SoundManager.Instance.PlaySfxLaser(0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxLaserHit(0.0f);

        if (character.IsBond())
        {
            character.AddHitPoint((-1) * _bondAdditionalDamage);
        }

        return true;
    }
}
