using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeowooBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 16f;
    private const float _damage = 5f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, true, true,
            _lifeTime, _speed, _damage);
    }
}
