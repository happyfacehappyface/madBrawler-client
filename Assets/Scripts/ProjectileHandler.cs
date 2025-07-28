using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ProjectileHandler : MonoBehaviour
{
    private List<Projectile> _projectiles;

    [SerializeField] private GameObject _sungjunProjectilePrefab;
    [SerializeField] private Transform _projectileParent;

    [SerializeField] private GameObject _sungjunBasicAttackProjectilePrefab;

    [SerializeField] private GameObject _sungjunSkill0Prefab;
    [SerializeField] private GameObject _sungjunSkill1Prefab;
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



    public void CreateSungjunBasicAttack(Team team, Direction direction)
    {
        GameObject newObject = CreateProjectile(
            _sungjunBasicAttackProjectilePrefab, GameController.Instance.GetPlayerTransform(team),
            Vector2.zero, direction, team);
        newObject.GetComponent<SungjunBasicAttackProjectile>().Initialize(direction);
    }

    public void CreateSungjunSkill0(Team team, float lifeTime)
    {
        GameObject newObject = CreateProjectile(
            _sungjunSkill0Prefab, GameController.Instance.GetPlayerSpinnedTransform(team),
            Vector3.zero, Direction.Right, team);
        newObject.GetComponent<SungjunSkill0Projectile>().Initialize(lifeTime);
    }

    public void CreateSungjunSkill1(Team team, Direction direction)
    {
        GameObject newObject = CreateProjectile(
            _sungjunSkill1Prefab, _projectileParent,
            GameController.Instance.GetPlayerTransform(team).position, direction, team);
        newObject.GetComponent<SungjunSkill1Projectile>().Initialize(direction);
    }

    public void CreateSungjunSkill2(Team team, Direction direction)
    {
        
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
