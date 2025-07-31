using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHandler : MonoBehaviour
{
    private Transform _wallParent;
    List<Wall> _walls;

    public void ManualStart(Transform wallParent)
    {
        _wallParent = wallParent;
        _walls = new List<Wall>();

        foreach (Transform child in _wallParent)
        {
            Wall wall = child.GetComponent<Wall>();
            if (wall != null)
            {
                wall.ManualStart();
                _walls.Add(wall);
            }
            
        }
    }

    public void ManualUpdate()
    {
        foreach (var wall in _walls)
        {
            wall.ManualUpdate();
        }
    }

}
