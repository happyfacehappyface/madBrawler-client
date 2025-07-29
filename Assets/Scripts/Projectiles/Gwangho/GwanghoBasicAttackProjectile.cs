using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwanghoBasicAttackProjectile : BasicProjectile
{
    private const float _lifeTime = 1.0f;
    private const float _speed = 12f;
    private const float _damage = 6f;

    private GameObject _lastHitWall;


    public void Initialize(Direction direction)
    {
        base.Initialize(
            direction, false, false, false,
            _lifeTime, 0f, _damage);
    }

    public override void ManualUpdate()
    {
        base.ManualUpdate();

        GraduallyMove(_speed * Time.deltaTime);
    }


    private void GraduallyMove(float power)
    {
        float step = 0.01f;
        int layerMask = LayerMask.GetMask("Wall", "Barrier");

        Vector3 rayDirection = Utils.DirectionToVector3(_direction);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 0.5f, layerMask);

        Color rayColor = hit.collider != null ? Color.green : Color.red;
        Debug.DrawRay(transform.position, rayDirection * 0.5f, rayColor, 0.1f);
        
        if (hit.collider != null)
        {
            Debug.Log("hit.collider.gameObject: " + hit.collider.gameObject.name);
            if ((_lastHitWall == null) || (hit.collider.gameObject != _lastHitWall))
            {
                Debug.Log("FlipByWall");
                _lastHitWall = hit.collider.gameObject;
                FlipByWall(hit.collider.gameObject.transform);
            }
        }

        transform.localPosition += Utils.DirectionToVector3(_direction) * Mathf.Min(power, step);

        if (power > step)
        {
            GraduallyMove(power - step);
        }
    }


    private void FlipByWall(Transform wall)
    {
        float dx = transform.position.x - wall.position.x;
        float dy = transform.position.y - wall.position.y;

        if (Mathf.Abs(dx) > Mathf.Abs(dy))
        {
            _direction = Utils.GetHorizontalFlippedDirection(_direction);
        }
        else
        {
            _direction = Utils.GetVerticalFlippedDirection(_direction);
        }
    }


    
}
