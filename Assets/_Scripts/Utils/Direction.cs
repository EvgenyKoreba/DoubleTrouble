using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Facing
{
    UP,
    RIGHT,
    DOWN,
    LEFT
}

public class Direction : MonoBehaviour
{
    public static Vector2 GetVector2(Facing facing)
    {
        switch (facing)
        {
            case Facing.UP:
                return Vector2.up;
            case Facing.RIGHT:
                return Vector2.right;
            case Facing.DOWN:
                return Vector2.down;
            case Facing.LEFT:
                return Vector2.left;
            default:
                Debug.LogError("Missing direction");
                return Vector2.zero;
        }
    }

    public static Facing GetNext(Facing facing)
    {
        switch (facing)
        {
            case Facing.UP:
                return Facing.RIGHT;
            case Facing.RIGHT:
                return Facing.DOWN;
            case Facing.DOWN:
                return Facing.LEFT;
            case Facing.LEFT:
                return Facing.UP;
            default:
                return default;
        }
    }

    public static Facing Reverse(Facing facing)
    {
        switch (facing)
        {
            case Facing.UP:
                return Facing.DOWN;
            case Facing.RIGHT:
                return Facing.LEFT;
            case Facing.DOWN:
                return Facing.UP;
            case Facing.LEFT:
                return Facing.RIGHT;
            default:
                return default;
        }
    }

    public static bool IsParallel(Facing first, Facing second)
    {
        if (IsHorizontal(first))
        {
            if (IsHorizontal(second))
            {
                return true;
            }
        }
        else if (IsVertical(first))
        {
            if (IsVertical(second))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsVertical(Facing facing)
    {
        return facing == Facing.UP || facing == Facing.DOWN;
    }

    public static bool IsHorizontal(Facing facing)
    {
        return facing == Facing.LEFT || facing == Facing.RIGHT;
    }
}
