using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotatingProjectile : Projectile
{
    protected float _angle;
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
        float angle, bool canBreakWall, bool isDeletedByWall, bool isDeletedByPlayer,
        float lifeTime, float speed, float damage)
    {
        _isHitByWall = false;
        _isHitByCharacter = false;
        _timeFromStart = TimeSpan.Zero;
        _angle = angle;
        _currentLifeTime = lifeTime;
        _currentSpeed = speed;
        _currentDamage = damage;
        HitIntervalTime = TimeSpan.FromSeconds(_currentLifeTime);
        _canBreakWall = canBreakWall;
        _isDeletedByWall = isDeletedByWall;
        _isDeletedByPlayer = isDeletedByPlayer;
        transform.localRotation = Utils.AngleToQuaternion(_angle);
    }

    

    public override void ManualUpdate()
    {
        base.ManualUpdate();
        _timeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
        transform.position += Time.deltaTime * _currentSpeed * Utils.AngleToVector3(_angle);
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

    public override void OnHitByWall(Wall wall)
    {
        if (!IsHarmful()) return;

        if (_canBreakWall)
        {
            wall.Deactivate();
        }
        
        _isHitByWall = true;
    }

    public override void OnHitByBarrier(Barrier barrier)
    {
        _isHitByWall = true;
    }
}
