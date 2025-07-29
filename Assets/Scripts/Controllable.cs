using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{
    private float _speed;

    private bool _isWallPassable;

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void Initialize(float speed, bool isWallPassable)
    {
        _speed = speed;
        _isWallPassable = isWallPassable;
    }

    public virtual void OnPressDirection(Direction direction)
    {
        GraduallyMove(direction, _speed * Time.deltaTime);

        //Direction = direction;
        //_spinnedTransform.localRotation = Utils.DirectionToQuaternion(direction);
    }

    private void GraduallyMove(Direction direction, float power)
    {
        float step = 0.01f;

        if (IsSomething(direction))
        {
            return;
        }

        transform.localPosition += Mathf.Min(power, step) * Utils.DirectionToVector3(direction);

        if (power > step)
        {
            GraduallyMove(direction, power - step);
        }
    }

    private bool IsSomething(Direction direction)
    {

        int layerMask = _isWallPassable ? LayerMask.GetMask("Barrier") : LayerMask.GetMask("Wall", "Barrier");
        

        if ((direction == Direction.Up) || (direction == Direction.UpLeft) || (direction == Direction.UpRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Up);
            RaycastHit2D hitUp = Physics2D.Raycast(transform.localPosition, rayDirection, 0.6f, layerMask);
            
            // Raycast 시각화 (빨간색: 충돌 없음, 초록색: 충돌 있음)
            Color rayColor = hitUp.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.6f, rayColor, 0.1f);
            
            if (hitUp.collider != null) return true;
        }

        if ((direction == Direction.Down) || (direction == Direction.DownLeft) || (direction == Direction.DownRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Down);
            RaycastHit2D hitDown = Physics2D.Raycast(transform.localPosition, rayDirection, 0.6f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitDown.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.6f, rayColor, 0.1f);
            
            if (hitDown.collider != null) return true;
        }

        if ((direction == Direction.Left) || (direction == Direction.UpLeft) || (direction == Direction.DownLeft))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Left);
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.localPosition, rayDirection, 0.4f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitLeft.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.4f, rayColor, 0.1f);
            
            if (hitLeft.collider != null) return true;
        }
        
        if ((direction == Direction.Right) || (direction == Direction.UpRight) || (direction == Direction.DownRight))
        {
            Vector3 rayDirection = Utils.DirectionToVector3(Direction.Right);
            RaycastHit2D hitRight = Physics2D.Raycast(transform.localPosition, rayDirection, 0.4f, layerMask);
            
            // Raycast 시각화
            Color rayColor = hitRight.collider != null ? Color.green : Color.red;
            Debug.DrawRay(transform.localPosition, rayDirection * 0.4f, rayColor, 0.1f);
            
            if (hitRight.collider != null) return true;
        }

        return false;
    }
}
