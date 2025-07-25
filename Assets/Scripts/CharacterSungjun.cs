using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSungjun : Character
{
    protected override void InitializeBaseStats()
    {
        HitPoint = 100;
        HitPointMax = 100;
        MoveSpeed = 2f;
        
        SpecialPoint = 0;
        SpecialPointMax = 100;
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

    public override void OnPressLeft()
    {
        base.OnPressLeft();
    }

    public override void OnPressRight()
    {
        base.OnPressRight();
    }

    public override void OnUseSkill1()
    {
        base.OnUseSkill1();
        Debug.Log("Sungjun OnUseSkill1");
        GameController.Instance.ProjectileHandler.CreateSungjunProjectile(GetPosition(), Utils.DirectionToVector2(GetDirection()), 10, Team);
    }

    
}
