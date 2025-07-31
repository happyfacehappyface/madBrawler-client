using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaehyeonSkill1Projectile : RotatingProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 13f;
    private const float _damage = 4f;

    public void Initialize(float angle)
    {
        base.Initialize(
            angle, false, true, true,
            _lifeTime, _speed, _damage);

        SoundManager.Instance.PlaySfxLaser(0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxLaserHit(0.0f);
        return true;
    }
}
