using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSungjun : Character
{
    public override int GetMaxHitPoint()
    {
        return 100;
    }

    public override float GetBaseMoveSpeed()
    {
        return 0f;
    }

    public override int GetMaxSpecialPoint()
    {
        return 100;
    }

    public CharacterSungjun()
    {

    }
    public override void OnPressUp()
    {
        base.OnPressUp();
        Debug.Log("Sungjun OnPressUp");
    }

    public override void OnPressDown()
    {
        base.OnPressDown();
        Debug.Log("Sungjun OnPressDown");
    }

    
}
