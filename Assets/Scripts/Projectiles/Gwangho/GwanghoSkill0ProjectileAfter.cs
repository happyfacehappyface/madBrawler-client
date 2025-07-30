using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GwanghoSkill0ProjectileAfter : BasicProjectile
{
    private const float _lifeTime = 0.5f;
    private const float _damage = 15f;

    private bool _isStun;

    public void Initialize(float scale, bool isStun)
    {
        base.Initialize(
            Direction.Right, true, false, false,
            _lifeTime, 0f, _damage);

        transform.localScale = new Vector3(scale, scale, scale);

        _isStun = isStun;

        SoundManager.Instance.PlaySfxDinosaurStomp(0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;

        if (_isStun)
        {
            character.AddEffect(GameController.Instance.CharacterEffectFactory.GwangHoSkill1DebuffStun(TimeSpan.FromSeconds(1.0f)));
        }

        return true;
    }
}
