using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MovementTypes
{
    public abstract class MovementBehaviour : MonoBehaviour
    {
        #region Fields
        [Header("Set in Inspector: MovingBehaviour")]
        [SerializeField] private bool _isLooping = false;
        [SerializeField] protected bool isChangeDirection = false;

        protected float timeStart = -1;
        protected float amountOfInterpolation;
        #endregion

        public bool IsLooping
        {
            get => _isLooping;
            set
            {
                _isLooping = value;
                StopAllCoroutines();
                Move();
            }
        }


        public void Move()
        {
            if (IsLooping)
            {
                LoopMove();
            }
            else
            {
                StraightMove();
            }
        }

        protected virtual void LoopMove()
        {
            StartCoroutine(LoopedMovement());
        }

        protected virtual IEnumerator LoopedMovement()
        {
            yield return null;
        }

        private void StraightMove()
        {
            StartCoroutine(Movement());
        }

        protected virtual IEnumerator Movement()
        {
            yield return null;
        }

        protected void TryChangeDirection()
        {
            if (isChangeDirection)
            {
                ChangeDirection();
            }
        }

        protected virtual void ChangeDirection() { }

        protected void SetPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }

        protected void SetPosition(Vector2 newPosition)
        {
            Vector3 temp = Vector3.zero;
            temp.x = newPosition.x;
            temp.y = newPosition.y;
            transform.position = temp;
        }

        protected Vector3 GetVector3_Position()
        {
            return transform.position;
        }

        protected Vector2 GetVector2_Position()
        {
            Vector2 temp = Vector2.zero;
            temp.x = transform.position.x;
            temp.y = transform.position.y;
            return temp;
        }
    }
}
