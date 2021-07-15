using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

namespace MovementTypes
{
    public class SinusoidalMovementPartData
    {
        [SerializeField] protected EasingCurve easingCurve = EasingCurve.Linear;

        [Range(1, 10)]
        [SerializeField] protected float easeMultiplier = 2f;

        [SerializeField] protected Facing direction = Facing.RIGHT;

        public EasingCurve EasingCurve
        {
            get => easingCurve;
        }

        public float EaseMultiplier
        {
            get => easeMultiplier;
        }

        public Facing Direction
        {
            get => direction;
        }


        public virtual void Reverse()
        {
            direction = global::Direction.Reverse(direction);
        }

        public Vector2 GetDirection()
        {
            return global::Direction.GetVector2(direction);
        }

        public bool IsVertical()
        {
            return global::Direction.IsVertical(direction);
        }
    }
}
