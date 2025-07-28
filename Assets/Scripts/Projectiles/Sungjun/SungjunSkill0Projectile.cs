using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SungjunSkill0Projectile : BasicProjectile
{
    private const float _damage = 10f;



    public void Initialize(float lifeTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            lifeTime, 0.0f, _damage);
    }

}
