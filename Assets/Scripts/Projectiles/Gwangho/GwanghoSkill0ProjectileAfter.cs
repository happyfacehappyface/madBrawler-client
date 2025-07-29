using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanghoSkill0ProjectileAfter : BasicProjectile
{
    private const float _lifeTime = 0.5f;
    private const float _damage = 15f;


    

    public void Initialize(float scale)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            _lifeTime, 0f, _damage);

        transform.localScale = new Vector3(scale, scale, scale);
    }
}
