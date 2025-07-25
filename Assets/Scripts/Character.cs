using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public virtual void OnPressUp()
    {
        Debug.Log("OnPressUp");
    }

    public virtual void OnPressDown()
    {
        Debug.Log("OnPressDown");
    }

    public virtual void OnPressLeft()
    {
        Debug.Log("OnPressLeft");
    }
    
    public virtual void OnPressRight()
    {
        Debug.Log("OnPressRight");
    }

    public virtual void OnUseSkill1()
    {
        Debug.Log("OnUseSkill1");
    }

    public virtual void OnUseSkill2()
    {
        Debug.Log("OnUseSkill2");
    }

    public virtual void OnUseSkill3()
    {
        Debug.Log("OnUseSkill3");
    }
    
}
