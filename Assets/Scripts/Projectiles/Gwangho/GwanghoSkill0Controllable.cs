using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanghoSkill0Controllable : Controllable
{
    private const float _speed = 10f;
    
    public void Initialize(float scale)
    {
        base.Initialize(_speed, true);
        transform.localScale = new Vector3(scale, scale, scale);
        SoundManager.Instance.PlaySfxDinosaurRoar(0.0f);
    }
}
