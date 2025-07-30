using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanghoSkill0Controllable : Controllable
{
    private const float _speed = 10f;
    
    public void Initialize()
    {
        base.Initialize(_speed, true);
        SoundManager.Instance.PlaySfxDinosaurRoar(0.0f);
    }
}
