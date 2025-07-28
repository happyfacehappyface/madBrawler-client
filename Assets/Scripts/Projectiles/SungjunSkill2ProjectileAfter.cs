using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill2ProjectileAfter : BasicProjectile
{
    private const float _damage = 30f;
    private float _delayTime;
    public void Initialize(float delayTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            delayTime + 0.2f, 0.0f, _damage);
        _delayTime = delayTime;
    }

    protected override bool IsHarmful()
    {
        return _timeFromStart > TimeSpan.FromSeconds(_delayTime);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        // TODO: 둔화 효과 추가
        
        return true;
    }
}
