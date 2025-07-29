using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 0.5f;
    private const float _speed = 18f;
    private const float _damageStart = 10f;
    private const float _damageDelta = -15f;
    private const float _scaleDelta = 2.5f;
    private const float _alphaDelta = -2.0f;
    private const float _harmfulTime = 0.4f;

    private float _scale = 0.5f;
    private float _alpha = 1.0f;

    



    [SerializeField] private SpriteRenderer _projectileSprite;

    

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, _speed, 0);

        _projectileSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);

        _currentDamage = _damageStart;
        _scale = 0.5f;
        _alpha = 1.0f;
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        _currentDamage += _damageDelta * Time.deltaTime;
        _scale += _scaleDelta * Time.deltaTime;
        _alpha += _alphaDelta * Time.deltaTime;

        _projectileSprite.color = new Color(1.0f, 1.0f, 1.0f, _alpha);
        transform.localScale = new Vector3(_scale, _scale, _scale);

    }

    protected override bool IsHarmful()
    {
        return _timeFromStart < TimeSpan.FromSeconds(_harmfulTime);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        character.AddHitPoint((-1) * _currentDamage);
        
        return true;
    }

}
