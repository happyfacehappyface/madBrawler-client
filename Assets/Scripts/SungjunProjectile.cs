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

    private bool _isHit;

    private TimeSpan _timeFromStart;



    public void Initialize(Vector2 direction)
    {
        _isHit = false;
        _timeFromStart = TimeSpan.Zero;
        _speed = 5;
        _direction = direction;
    }

    

    public override void ManualUpdate()
    {
        _timeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
        transform.position += Time.deltaTime * _speed * Utils.Vector2ToVector3(_direction);
    }

    public override bool ShouldBeDestroyed()
    {
        return (_timeFromStart > _lifeTime) || _isHit;
    }

    public override void OnHitByCharacter(Character character)
    {
        if (character.GetTeam() == Team)
        {
            return;
        }

        _isHit = true;
    }

    public override void OnHitByWall()
    {
        _isHit = true;
    }
    
}
