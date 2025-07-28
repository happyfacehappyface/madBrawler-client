using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill2ProjectileAfter : BasicProjectile
{
    private const float _damage = 30f;
    public void Initialize(float delayTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            delayTime + 0.2f, delayTime, 0.0f, _damage);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        // TODO: 둔화 효과 추가
        
        return true;
    }
}
