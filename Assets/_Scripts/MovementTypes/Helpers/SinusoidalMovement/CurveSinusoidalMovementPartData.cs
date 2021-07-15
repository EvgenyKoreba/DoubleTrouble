using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;

namespace MovementTypes
{
    [System.Serializable]
    public class CurveSinusoidalMovementPartData : SinusoidalMovementPartData
    {
        [Range(1,20)]
        [SerializeField] private float _amplitude = 2f;
        [SerializeField] private float _angle = 1f;

        public float Angle
        {
            get => _angle;
        }

        public float Amplitude
        {
            get => _amplitude;
        }

        public void PrepareAngle()
        {
            if (_angle <= 180)
            {
                _angle = 180;
            }
            else
            {
                int amount = (int)_angle / 180;
                _angle = 180 * (amount);
            }
        }


        public void SetNextDirection()
        {
            direction = global::Direction.GetNext(direction);
        }

        public override void Reverse()
        {
            if (_angle % 360 == 0)
            {
                direction = global::Direction.Reverse(direction);
            }
        }
    }
}
