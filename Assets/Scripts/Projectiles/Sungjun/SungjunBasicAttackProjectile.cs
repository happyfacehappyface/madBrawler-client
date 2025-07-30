using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SungjunBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 0.15f;
    private const float _speed = 18f;
    private const float _damage = 10f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, _speed, _damage);

        SoundManager.Instance.PlaySfxWind(0.0f);
    }


    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxPunch(0.0f);
        return true;
    }

}
