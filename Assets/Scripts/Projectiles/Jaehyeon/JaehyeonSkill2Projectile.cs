using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JaehyeonSkill2Projectile : RotatingProjectile
{
    [SerializeField] private BoxCollider2D _collider2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private const float _lifeTime = 3.0f;
    private const float _extendTime = 0.5f;
    private const float _collapseTime = 0.5f;
    private const float _damage = 15f;
    private const float _bondDuration = 2.0f;

    private Vector2 _origin;
    private Vector2 _destination;
    private Vector2 _center;
    private float _distance;

    public void Initialize(Vector2 origin, Vector2 destination)
    {
        _origin = origin;
        _destination = destination;
        _center = (origin + destination) / 2f;
        float angle = Mathf.Atan2(destination.y - origin.y, destination.x - origin.x) * Mathf.Rad2Deg;
        _distance = Vector2.Distance(origin, destination);

        base.Initialize(
            angle, false, false, false,
            _lifeTime, 0f, _damage);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        if (_timeFromStart < TimeSpan.FromSeconds(_extendTime))
        {
            float progress = (float)(_timeFromStart.TotalSeconds / _extendTime);
            _collider2D.size = new Vector2(_distance * progress, 0.3f);
            _spriteRenderer.size = new Vector2(_distance * progress, 1f);
            transform.position = Vector2.Lerp(_origin, _center, progress);
        }
        else if (_timeFromStart < TimeSpan.FromSeconds(_lifeTime - _collapseTime))
        {
            _collider2D.size = new Vector2(_distance, 0.3f);
            _spriteRenderer.size = new Vector2(_distance, 1f);
            transform.position = _center;
        }
        else
        {
            float progress = (float)(_timeFromStart.TotalSeconds - (_lifeTime - _collapseTime)) / _collapseTime;
            _collider2D.size = new Vector2(_distance * (1 - progress), 0.3f);
            _spriteRenderer.size = new Vector2(_distance * (1 - progress), 1f);
            transform.position = Vector2.Lerp(_center, _destination, progress);
        }

    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        character.AddEffect(GameController.Instance.CharacterEffectFactory.JaehyeonSkill2DebuffBond(TimeSpan.FromSeconds(_bondDuration)));

        return true;
    }
}
