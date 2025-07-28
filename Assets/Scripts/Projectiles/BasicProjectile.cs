using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicProjectile : Projectile
{
    protected Direction _direction;
    private bool _isHitByWall;
    private bool _isHitByCharacter;
    protected TimeSpan _timeFromStart;
    


    private float _lifeTime;
    private float _speed;
    private float _damage;

    private bool _canBreakWall;
    private bool _isDeletedByWall;

    private bool _isDeletedByPlayer;





    protected void Initialize(
        Direction direction, bool canBreakWall, bool isDeletedByWall, bool isDeletedByPlayer,
        float lifeTime, float speed, float damage)
    {
        _isHitByWall = false;
        _isHitByCharacter = false;
        _timeFromStart = TimeSpan.Zero;
        _direction = direction;
        _lifeTime = lifeTime;
        _speed = speed;
        _damage = damage;
        HitIntervalTime = TimeSpan.FromSeconds(_lifeTime);
        _canBreakWall = canBreakWall;
        _isDeletedByWall = isDeletedByWall;
        _isDeletedByPlayer = isDeletedByPlayer;
        transform.localRotation = Utils.DirectionToQuaternion(direction);
    }

    

    public override void ManualUpdate()
    {
        _timeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
        transform.position += Time.deltaTime * _speed * Utils.DirectionToVector3(_direction);
    }

    protected virtual bool IsHarmful()
    {
        return true;
    }

    public override bool ShouldBeDestroyed()
    {
        return (_timeFromStart > TimeSpan.FromSeconds(_lifeTime))
        || (_isDeletedByWall && _isHitByWall)
        || (_isDeletedByPlayer && _isHitByCharacter);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!IsHarmful()) return false;
        if (!base.OnHitByCharacter(character)) return false;

        _isHitByCharacter = true;
        character.AddHitPoint((-1) * _damage);
        return true;
    }

    public override void OnHitByWall(Wall wall)
    {
        if (!IsHarmful()) return;

        if (_canBreakWall)
        {
            wall.Deactivate();
        }
        
        _isHitByWall = true;
    }
}
