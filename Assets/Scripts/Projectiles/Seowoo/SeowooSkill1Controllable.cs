using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeowooSkill1Controllable : Controllable
{
    private const float _speed = 0f;
    
    public void Initialize()
    {
        base.Initialize(_speed, true);
    }
}
