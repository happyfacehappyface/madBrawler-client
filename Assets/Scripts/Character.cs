using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    protected Direction Direction;
    protected Team Team;

    protected int HitPoint;
    protected int HitPointMax;
    protected float MoveSpeed;
    protected int SpecialPoint;
    protected int SpecialPointMax;

    public virtual void ManualStart(Team team)
    {
        InitializeBaseStats();
        Team = team;
    }

    public float HitPointRatio()
    {
        return (float)HitPoint / (float)HitPointMax;
    }


    public Vector2 GetPosition()
    {
        return transform.localPosition;
    }

    public Direction GetDirection()
    {
        return Direction;
    }

    public Team GetTeam()
    {
        return Team;
    }


    protected void AddHitPoint(int amount)
    {
        HitPoint = Mathf.Clamp(HitPoint + amount, 0, HitPointMax);
    }

    protected void AddSpecialPoint(int amount)
    {
        SpecialPoint = Mathf.Clamp(SpecialPoint + amount, 0, SpecialPointMax);
    }

    protected abstract void InitializeBaseStats();

    public virtual void OnPressUp()
    {
        transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(0, 1, 0);
        Direction = Direction.Up;
    }

    public virtual void OnPressDown()
    {
        transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(0, -1, 0);
        Direction = Direction.Down;
    }

    public virtual void OnPressLeft()
    {
        transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(-1, 0, 0);
        Direction = Direction.Left;
    }
    
    public virtual void OnPressRight()
    {
        transform.localPosition += Time.deltaTime * MoveSpeed * new Vector3(1, 0, 0);
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

    public virtual void OnHitByProjectile(Projectile projectile)
    {

        if (projectile.Team == Team)
        {
            return;
        }

        AddHitPoint((-1) * projectile.Damage);
        Debug.Log("OnHitByProjectile");
    }
    
}


public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
