using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private List<Projectile> _projectiles;

    [SerializeField] private GameObject _sungjunProjectilePrefab;
    [SerializeField] private Transform _sungjunProjectileParent;


    public void ManualStart()
    {
        _projectiles = new List<Projectile>();
    }

    public void ManualUpdate()
    {
        CleanUpAndUpdateProjectiles();
    }

    private void CleanUpAndUpdateProjectiles()
    {
        for (var i = _projectiles.Count - 1; i >= 0; i--)
        {
            var projectile = _projectiles[i];
            if (projectile.ShouldBeDestroyed())
            {
                projectile.OnDestroy();
                _projectiles.RemoveAt(i);
            }
            else
            {
                projectile.ManualUpdate();
            }
        }
    }

    public void CreateSungjunProjectile(Vector2 origin, Vector2 direction, float speed, Team team)
    {
        GameObject newObject = Instantiate(_sungjunProjectilePrefab, _sungjunProjectileParent);
        Debug.Log($"CreateSungjunProjectile: {origin}, {direction}, {speed}");

        newObject.transform.SetLocalPositionAndRotation(Utils.Vector2ToVector3(origin), Quaternion.Euler(Utils.Vector2ToVector3(direction)));

        SungjunProjectile newProjectile = newObject.GetComponent<SungjunProjectile>();
        newProjectile.ManualStart(team);
        newProjectile.Initialize(direction);

        _projectiles.Add(newProjectile);
    }


}
