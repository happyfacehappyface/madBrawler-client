using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanghoSkill1Projectile : BasicProjectile
{
    private const float _damage = 10f;



    public void Initialize(float duration)
    {
        base.Initialize(
            Direction.Right, false, false, false,
            duration, 0.0f, _damage);
        
        SoundManager.Instance.PlaySfxRollingChair(0.0f);
    }
}
