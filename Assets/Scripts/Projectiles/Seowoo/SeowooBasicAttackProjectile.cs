using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeowooBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 2.0f;
    private const float _speed = 16f;
    private const float _damage = 2f;

    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, true, true,
            _lifeTime, _speed, _damage);

        SoundManager.Instance.PlaySfxFireball(0.0f);
    }

    public override bool OnHitByCharacter(Character character)
    {
        if (!base.OnHitByCharacter(character)) return false;
        SoundManager.Instance.PlaySfxFireballHit(0.0f);
        GameController.Instance.ProjectileHandler.CreateSeowooBasicAttackAfter(Team, Direction.Right, transform.position);
        return true;
    }

    public override bool OnHitByWall(Wall wall)
    {
        if (!base.OnHitByWall(wall)) return false;
        SoundManager.Instance.PlaySfxFireballHit(0.0f);
        GameController.Instance.ProjectileHandler.CreateSeowooBasicAttackAfter(Team, Direction.Right, transform.position);
        return true;
    }

    public override bool OnHitByBarrier(Barrier barrier)
    {
        if (!base.OnHitByBarrier(barrier)) return false;
        SoundManager.Instance.PlaySfxFireballHit(0.0f);
        GameController.Instance.ProjectileHandler.CreateSeowooBasicAttackAfter(Team, Direction.Right, transform.position);
        return true;
    }
}
