using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{

    public GameObject PlayerObject;
    public Direction Direction;

    protected int HitPoint;
    protected float MoveSpeed;
    protected int SpecialPointMax;


    

    public void SetPlayerObject(GameObject playerObject)
    {
        PlayerObject = playerObject;
    }

    public Vector2 GetPosition()
    {
        return PlayerObject.transform.localPosition;
    }

    public Direction GetDirection()
    {
        return Direction;
    }

    protected abstract void InitializeBaseStats();

    public virtual void OnPressUp()
    {
        PlayerObject.transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(0, 1, 0);
        Debug.Log($"move Speed: {MoveSpeed}");
        Direction = Direction.Up;
    }

    public virtual void OnPressDown()
    {
        PlayerObject.transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(0, -1, 0);
        Direction = Direction.Down;
    }

    public virtual void OnPressLeft()
    {
        PlayerObject.transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(-1, 0, 0);
        Direction = Direction.Left;
    }
    
    public virtual void OnPressRight()
    {
        PlayerObject.transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(1, 0, 0);
        Direction = Direction.Right;
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


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
