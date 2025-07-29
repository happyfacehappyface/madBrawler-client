using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaehyeonBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 5.0f;
    private const float _speed = 1f;
    private const float _speedAcceleration = 3f;
    private const float _damage = 5f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, true, true,
            _lifeTime, _speed, _damage);
    }


    public override void ManualUpdate()
    {
        base.ManualUpdate();

        _currentSpeed += _speedAcceleration * Time.deltaTime;
    }
}
