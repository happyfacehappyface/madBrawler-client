using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
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
}
