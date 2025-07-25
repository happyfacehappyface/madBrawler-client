using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    public List<Projectile> Projectiles;

    [SerializeField] private GameObject _sungjunProjectilePrefab;
    [SerializeField] private Transform _sungjunProjectileParent;


    public void ManualStart()
    {
        Projectiles = new List<Projectile>();
    }

    public void ManualUpdate()
    {
        CleanUpAndUpdateProjectiles();
    }

    private void CleanUpAndUpdateProjectiles()
    {
        for (var i = Projectiles.Count - 1; i >= 0; i--)
        {
            var projectile = Projectiles[i];
            if (projectile.ShouldBeDestroyed())
            {
                projectile.OnDestroy();
                Projectiles.RemoveAt(i);
            }
            else
            {
                projectile.ManualUpdate();
            }
        }
    }

    public void CreateSungjunProjectile(Vector2 origin, Vector2 direction, float speed, int damage)
    {
        GameObject newObject = Instantiate(_sungjunProjectilePrefab, _sungjunProjectileParent);

        newObject.transform.SetLocalPositionAndRotation(Utils.Vector2ToVector3(origin), Quaternion.Euler(Utils.Vector2ToVector3(direction)));

        Projectile newProjectile = new SungjunProjectile(direction, speed, damage);
        newProjectile.SetProjectileObject(newObject);

        Projectiles.Add(newProjectile);
    }


}
