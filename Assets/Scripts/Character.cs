using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{

    public GameObject PlayerObject;

    public int HitPoint;
    public int MoveSpeed;
    public int SpecialPointMax;


    public abstract int GetMaxHitPoint();
    public abstract float GetBaseMoveSpeed();
    public abstract int GetMaxSpecialPoint();

    

    public void SetPlayerObject(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }

    public virtual void OnPressUp()
    {
        PlayerObject.transform.position += Time.deltaTime * GetBaseMoveSpeed() * new Vector3(0, 1, 0);
    }

    public virtual void OnPressDown()
    {
        PlayerObject.transform.position += Time.deltaTime * GetBaseMoveSpeed() * new Vector3(0, -1, 0);
    }

    public virtual void OnPressLeft()
    {
        PlayerObject.transform.position += Time.deltaTime * GetBaseMoveSpeed() * new Vector3(-1, 0, 0);
    }
    
    public virtual void OnPressRight()
    {
        PlayerObject.transform.position += Time.deltaTime * GetBaseMoveSpeed() * new Vector3(1, 0, 0);
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
