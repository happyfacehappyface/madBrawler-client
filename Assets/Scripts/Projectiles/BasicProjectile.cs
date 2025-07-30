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
    


    protected float _currentLifeTime;
    protected float _currentSpeed;
    protected float _currentDamage;

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
        _currentLifeTime = lifeTime;

        if (speed == 0f)
        {
            _currentSpeed = 0.001f;
        }
        else
        {
            _currentSpeed = speed;
        }

        _currentDamage = damage;
        HitIntervalTime = TimeSpan.FromSeconds(_currentLifeTime);
        _canBreakWall = canBreakWall;
        _isDeletedByWall = isDeletedByWall;
        _isDeletedByPlayer = isDeletedByPlayer;
        transform.localRotation = Utils.DirectionToQuaternion(direction);
    }

    

    public override void ManualUpdate()
    {
        base.ManualUpdate();
        _timeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
        transform.position += Time.deltaTime * _currentSpeed * Utils.DirectionToVector3(_direction);
    }

    protected override bool IsHarmful()
    {
        return true;
    }

    public override bool ShouldBeDestroyed()
    {
        return (_timeFromStart > TimeSpan.FromSeconds(_currentLifeTime))
        || (_isDeletedByWall && _isHitByWall)
        || (_isDeletedByPlayer && _isHitByCharacter);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        _isHitByCharacter = true;
        character.AddHitPoint((-1) * _currentDamage);
        return true;
    }

    public override bool OnHitByWall(Wall wall)
    {
        if (!IsHarmful()) return false;

        if (_canBreakWall)
        {
            wall.Deactivate();
        }
        
        _isHitByWall = true;
        return true;
    }

    public override bool OnHitByBarrier(Barrier barrier)
    {
        _isHitByWall = true;
        return true;
    }
}
