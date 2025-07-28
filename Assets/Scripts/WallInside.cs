using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInside : MonoBehaviour
{

    private Wall _wall;
    public void ManualStart(Wall wall)
    {
        _wall = wall;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out Character character))
        {
            _wall.Deactivate();
        }
    }
}
