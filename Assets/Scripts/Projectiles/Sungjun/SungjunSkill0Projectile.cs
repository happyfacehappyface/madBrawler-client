using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SungjunSkill0Projectile : BasicProjectile
{
    private const float _damage = 4f;



    public void Initialize(float lifeTime)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            lifeTime, 0.0f, _damage);

        SoundManager.Instance.PlaySfxWind(0.0f);

        HitIntervalTime = TimeSpan.FromSeconds(0.5f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxBump(0.0f);
        character.ChangeStateForcedMove(_owner.GetDirection(), 8f, TimeSpan.FromSeconds(0.3f), true, true);
        return true;
    }


}
