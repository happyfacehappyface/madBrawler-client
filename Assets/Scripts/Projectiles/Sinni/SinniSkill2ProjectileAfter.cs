using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinniSkill2ProjectileAfter : BasicProjectile
{
    private const float _lifeTime = 3.0f;

    public void Initialize()
    {
        base.Initialize(
            Direction.Right, false, false, false,
            _lifeTime, 0.0f, 0f);

        SoundManager.Instance.PlaySfxDebris(0.0f);
    }
}
