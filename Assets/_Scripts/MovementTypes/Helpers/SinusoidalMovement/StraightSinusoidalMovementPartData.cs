using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;


namespace MovementTypes
{
    [System.Serializable]
    public class StraightSinusoidalMovementPartData : SinusoidalMovementPartData
    {
        [Range(0.5f, 20f)]
        [SerializeField] protected float speed = 1f;


        public float Speed
        {
            get => speed;
        }
    }

}
