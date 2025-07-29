using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static void Log(string message)
    {
        #if UNITY_EDITOR
        Debug.Log(message);
        #endif
    }

    public static Vector3 Vector2ToVector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, vector2.y, 0);
    }

    public static Vector2 DirectionToVector2(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector2(0, 1);
            case Direction.Down:
                return new Vector2(0, -1);
            case Direction.Left:
                return new Vector2(-1, 0);
            case Direction.Right:
                return new Vector2(1, 0);
            case Direction.UpLeft:
                return new Vector2(-1, 1).normalized;
            case Direction.UpRight:
                return new Vector2(1, 1).normalized;
            case Direction.DownLeft:
                return new Vector2(-1, -1).normalized;
            case Direction.DownRight:
                return new Vector2(1, -1).normalized;
            default:
                return Vector2.zero;
        }
    }

    public static Vector3 DirectionToVector3(Direction direction)
    {
        return Vector2ToVector3(DirectionToVector2(direction));
    }

    public static Quaternion DirectionToQuaternion(Direction direction)
    {
        float angle = 0f;
        switch (direction)
        {
            case Direction.Right:
                angle = 0f;
                break;
            case Direction.UpRight:
                angle = 45f;
                break;
            case Direction.Up:
                angle = 90f;
                break;
            case Direction.UpLeft:
                angle = 135f;
                break;
            case Direction.Left:
                angle = 180f;
                break;
            case Direction.DownLeft:
                angle = 225f;
                break;
            case Direction.Down:
                angle = 270f;
                break;
            case Direction.DownRight:
                angle = 315f;
                break;
            default:
                angle = 0f;
                break;
        }


        return Quaternion.Euler(0f, 0f, angle);
    }

    public static Direction GetVerticalFlippedDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Down;
            case Direction.Down:
                return Direction.Up;
            case Direction.Left:
                return Direction.Left;
            case Direction.Right:
                return Direction.Right;
            case Direction.UpLeft:
                return Direction.DownLeft;
            case Direction.UpRight:
                return Direction.DownRight;
            case Direction.DownLeft:
                return Direction.UpLeft;
            case Direction.DownRight:
                return Direction.UpRight;
            default:
                return direction;
        }
    }

    public static Direction GetHorizontalFlippedDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Direction.Up;
            case Direction.Down:
                return Direction.Down;
            case Direction.Left:
                return Direction.Right;
            case Direction.Right:
                return Direction.Left;
            case Direction.UpLeft:
                return Direction.UpRight;
            case Direction.UpRight:
                return Direction.UpLeft;
            case Direction.DownLeft:
                return Direction.DownRight;
            case Direction.DownRight:
                return Direction.DownLeft;
            default:
                return direction;
        }
    }
}
