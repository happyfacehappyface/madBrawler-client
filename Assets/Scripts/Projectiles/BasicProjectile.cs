using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicProjectile : Projectile
{
    private Vector2 _direction;
    private bool _isHit;
    private TimeSpan _timeFromStart;


    [SerializeField] private float _lifeTime;
    [SerializeField] private float _speed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _damage;

    private bool _canBreakWall;





    public void Initialize(Vector2 direction, bool canBreakWall)
    {
        _isHit = false;
        _timeFromStart = TimeSpan.Zero;
        _direction = direction;
        HitIntervalTime = TimeSpan.FromSeconds(_lifeTime);
        _canBreakWall = canBreakWall;
    }

    

    public override void ManualUpdate()
    {
        _timeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
        transform.position += Time.deltaTime * _speed * Utils.Vector2ToVector3(_direction);
        
    }

    public override bool ShouldBeDestroyed()
    {
        return (_timeFromStart > TimeSpan.FromSeconds(_lifeTime));
        //return (_timeFromStart > TimeSpan.FromSeconds(_lifeTime)) || _isHit;
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        _isHit = true;
        character.AddHitPoint((-1) * _damage);
        return true;
    }

    public override void OnHitByWall(Wall wall)
    {
        if (_canBreakWall)
        {
            wall.Deactivate();
        }
        
        _isHit = true;
    }
}
