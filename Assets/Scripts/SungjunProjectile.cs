using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class SungjunProjectile : Projectile
{

    private TimeSpan _lifeTime = TimeSpan.FromSeconds(10);

    private Vector2 _direction;
    private float _speed;
    private int _damage;

    private bool _isHit = false;


    public SungjunProjectile(Vector2 direction, float speed, int damage)
    {
        _direction = direction;
        _speed = speed;
        _damage = damage;
    }

    

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        ProjectileObject.transform.position += Time.deltaTime * _speed * new Vector3(_direction.x, _direction.y, 0);
    }

    public override bool ShouldBeDestroyed()
    {
        return (TimeFromStart > _lifeTime) || _isHit;
    }

    public override void OnHitByCharacter(Character character)
    {
        _isHit = true;
    }

    public override void OnHitByWall()
    {
        _isHit = true;
    }
    
}
