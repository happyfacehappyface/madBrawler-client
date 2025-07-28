using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniSkill0Projectile : BasicProjectile
{
    private const float _lifeTime = 5.0f;
    private const float _damage = 2f;


    

    public void Initialize(Direction direction, float scale)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, 0f, _damage);

        transform.localScale = new Vector3(scale, scale, scale);

        HitIntervalTime = TimeSpan.FromSeconds(0.1f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        
        
        return true;
    }

    

}
