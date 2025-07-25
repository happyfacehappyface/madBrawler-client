using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSungjun : Character
{
    
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
