using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableHandler : MonoBehaviour
{

    [SerializeField] private Transform _controllableParent;
    [SerializeField] private GameObject _gwanghoSkill0ControllablePrefab;

    [SerializeField] private GameObject _seowooSkill1ControllablePrefab;

    [SerializeField] private GameObject _jaehyeonSkill0ControllablePrefab;

    public void ManualStart()
    {

    }

    private GameObject CreateControllable(GameObject prefab, Transform parent, Vector2 origin)
    {
        GameObject newObject = Instantiate(prefab, parent);

        newObject.transform.localPosition = Utils.Vector2ToVector3(origin);

        //Controllable newControllable = newObject.GetComponent<Controllable>();
        //newControllable.ManualStart(origin, _speed, true);

        //IncrementProjectileID(team);

        //_projectiles.Add(newProjectile);

        return newObject;
    }

    public Controllable CreateGwanghoSkill0(Team team, float scale)
    {
        GameObject newObject = CreateControllable(
            _gwanghoSkill0ControllablePrefab, _controllableParent, GameController.Instance.GetPlayerTransform(team).position);
        newObject.GetComponent<GwanghoSkill0Controllable>().Initialize(scale);
        return newObject.GetComponent<GwanghoSkill0Controllable>();
    }

    public Controllable CreateSeowooSkill1(Team team)
    {
        GameObject newObject = CreateControllable(
            _seowooSkill1ControllablePrefab, _controllableParent, GameController.Instance.GetPlayerTransform(team).position);
        newObject.GetComponent<SeowooSkill1Controllable>().Initialize();
        return newObject.GetComponent<SeowooSkill1Controllable>();
    }

    public Controllable CreateJaehyeonSkill0(Team team)
    {
        GameObject newObject = CreateControllable(
            _jaehyeonSkill0ControllablePrefab, _controllableParent, GameController.Instance.GetPlayerTransform(team).position);
        newObject.GetComponent<JaehyeonSkill0Controllable>().Initialize();
        return newObject.GetComponent<JaehyeonSkill0Controllable>();
    }
}
