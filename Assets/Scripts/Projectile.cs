using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public abstract class Projectile : MonoBehaviour
{
    private int _damage = 2;
    public int Damage => _damage;

    
    private Team _team;
    public Team Team => _team;


    public abstract bool ShouldBeDestroyed();

    public abstract void OnHitByCharacter(Character character);

    public abstract void OnHitByWall();

    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }

    public virtual void ManualStart(Team team)
    {
        _team = team;
    }

    public abstract void ManualUpdate();


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            OnHitByCharacter(character);
            character.OnHitByProjectile(this);
        }

        // if (other.TryGetComponent<Wall>(out Wall wall))
        // {
        //     OnHitByWall();
        // }
    }

}



