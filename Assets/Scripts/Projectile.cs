using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Projectile
{
    public GameObject ProjectileObject;
    public TimeSpan TimeFromStart;

    public void SetProjectileObject(GameObject projectileObject)
    {
        ProjectileObject = projectileObject;
    }


    public virtual void ManualStart()
    {
        TimeFromStart = TimeSpan.Zero;
    }
    public virtual void ManualUpdate()
    {
        TimeFromStart += TimeSpan.FromSeconds(Time.deltaTime);
    }

    public abstract bool ShouldBeDestroyed();

    public abstract void OnHitByCharacter(Character character);

    public abstract void OnHitByWall();


    public virtual void OnDestroy()
    {
        UnityEngine.Object.Destroy(ProjectileObject);
    }

}



