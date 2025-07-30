using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill2Projectile : BasicProjectile
{
    private float _bounceTime;
    public void Initialize(float bounceTime)
    {
        base.Initialize(
            Direction.Right, false, false, false,
            0.1f, 0.0f, 0.0f);
        _bounceTime = bounceTime;
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        
        character.ChangeStateForcedMove(_direction, 0, TimeSpan.FromSeconds(_bounceTime), true, true);
        SoundManager.Instance.PlaySfxKarate(0.0f);
        return true;
    }
}
