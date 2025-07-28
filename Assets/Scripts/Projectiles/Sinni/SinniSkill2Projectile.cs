using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SinniSkill2Projectile : BasicProjectile
{
    public void Initialize()
    {
        base.Initialize(
            Direction.Right, false, false, true,
            0.1f, 0.0f, 0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        
        //character.ChangeStateForcedMove(_direction, 0, TimeSpan.FromSeconds(_bounceTime), true, true);

        GameController.Instance.ProjectileHandler.CreateSinniSkill2After(Team, _direction);

        
        return true;
    }
}
