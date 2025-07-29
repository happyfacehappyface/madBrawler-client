using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class Projectile : MonoBehaviour
{

    public int ProjectileID { get; private set; }
    public TimeSpan HitIntervalTime { get; protected set; }
    private Team _team;
    public Team Team => _team;

    protected Character _owner;


    public abstract bool ShouldBeDestroyed();

    public virtual bool OnHitByCharacter(Character character)
    {
        if (Team == character.GetTeam()) return false;
        if (!character.CanHitByProjectile(this)) return false;

        character.OnHitByProjectile(this);
        return true;
    }

    public abstract void OnHitByWall(Wall wall);
    public abstract void OnHitByBarrier(Barrier barrier);

    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void ManualStart(Team team, int projectileID, Character owner)
    {
        _team = team;
        _owner = owner;
        ProjectileID = projectileID;
        
        if (gameObject.layer != LayerMask.NameToLayer("Wall") && 
            gameObject.layer != LayerMask.NameToLayer("Barrier"))
        {
            gameObject.layer = LayerMask.NameToLayer(_team == Team.Left ? "LeftProjectile" : "RightProjectile");
        }
    }

    public abstract void ManualUpdate();


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            OnHitByCharacter(character);
        }

        if (other.TryGetComponent<Wall>(out Wall wall))
        {
            OnHitByWall(wall);
        }

        if (other.TryGetComponent<Barrier>(out Barrier barrier))
        {
            OnHitByBarrier(barrier);
        }
    }

}



