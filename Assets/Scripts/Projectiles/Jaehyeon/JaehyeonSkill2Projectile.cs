using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaehyeonSkill2Projectile : RotatingProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 11f;
    private const float _damage = 3f;

    private float _distance;

    public void Initialize(float angle, float distance)
    {
        base.Initialize(
            angle, false, true, true,
            _lifeTime, _speed, _damage);
        _distance = distance;
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        return true;
    }
}
