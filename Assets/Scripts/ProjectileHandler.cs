using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler : MonoBehaviour
{
    private List<Projectile> _projectiles;

    [SerializeField] private GameObject _sungjunProjectilePrefab;
    [SerializeField] private Transform _projectileParent;

    [SerializeField] private GameObject _sungjunSkill2Prefab;

    int _leftProjectileID;
    int _rightProjectileID;


    public void ManualStart()
    {
        _projectiles = new List<Projectile>();
        _leftProjectileID = 0;
        _rightProjectileID = 0;
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
        GameObject newObject = Instantiate(_sungjunProjectilePrefab, _projectileParent);

        newObject.transform.SetLocalPositionAndRotation(Utils.Vector2ToVector3(origin), Quaternion.Euler(Utils.Vector2ToVector3(direction)));

        BasicProjectile newProjectile = newObject.GetComponent<BasicProjectile>();
        newProjectile.ManualStart(team, team == Team.Left ? _leftProjectileID : _rightProjectileID);
        newProjectile.Initialize(direction, false);

        _projectiles.Add(newProjectile);

        IncrementProjectileID(team);
    }

    private GameObject CreateProjectile(GameObject prefab, Transform parent, Vector2 origin, Direction direction, Team team)
    {
        GameObject newObject = Instantiate(prefab, parent);

        newObject.transform.SetLocalPositionAndRotation(Utils.Vector2ToVector3(origin), Quaternion.Euler(Utils.DirectionToVector3(direction)));

        Projectile newProjectile = newObject.GetComponent<Projectile>();
        newProjectile.ManualStart(team, team == Team.Left ? _leftProjectileID : _rightProjectileID);

        IncrementProjectileID(team);

        _projectiles.Add(newProjectile);

        return newObject;
    }

    public void CreateSungjunSkill2(Team team)
    {
        GameObject newObject = CreateProjectile(
            _sungjunSkill2Prefab, GameController.Instance.GetPlayerTransform(team),
            Vector3.zero, Direction.Right, team);
        newObject.GetComponent<BasicProjectile>().Initialize(Utils.DirectionToVector2(Direction.Right), true);
    }



    private void IncrementProjectileID(Team team)
    {
        if (team == Team.Left)
        {
            _leftProjectileID++;
        }
        else
        {
            _rightProjectileID++;
        }
    }


}
