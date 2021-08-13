using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerPhysics : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;

        public float Drag
        {
            get => _rigidbody.drag;
            set => _rigidbody.drag = value;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void NullifyVerticalVelocity()
        {
            Vector3 velocity = _rigidbody.velocity;
            velocity.y = 0;
            _rigidbody.velocity = velocity;
        }
    }

}
