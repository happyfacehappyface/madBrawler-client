using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HookProjectile : BasicProjectile
{

    private float _hookPower;
    private TimeSpan _hookTime;
    public void Initialize(float hookPower, TimeSpan hookTime)
    {
        _hookPower = hookPower;
        _hookTime = hookTime;
        
    }

    public override bool OnHitByCharacter(Character character)
    {
        base.OnHitByCharacter(character);
        character.ChangeStateForcedMove(_direction, _hookPower, true, _hookTime);
        return true;
    }
}
