using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace Project.Player
{

    public class PlayerHealth : MonoBehaviour, IHealthChangeHandler, IPickUpItem
    {
        [Header("Set in Inspector")]
        [SerializeField] private int _maxLifes;

        [Header("Set Dynamically")]
        [SerializeField] private int _currentLifes;

        private void Awake()
        {
            CurrentLifes = _maxLifes;
        }

        #region Properties
        public int CurrentLifes
        {
            get { return _currentLifes; }
            private set {
                if (value <= 0)
                {
                    RaiseDieEvent();
                }

                _currentLifes = Mathf.Clamp(value, 1, _maxLifes);
            }
        }

        private void RaiseDieEvent()
        {
            EventsHandler.RaiseEvent<IRestartLevelHandler>(r => r.RestartLevel());
        }

        public int MaxLifes
        {
            get => _maxLifes;
        }
        #endregion

        #region Events
        private void OnEnable()
        {
            EventsHandler.Subscribe(this);
        }

        private void OnDisable()
        {
            EventsHandler.Unsubscribe(this);
        }

        public void Heal(int value = 1)
        {
            CurrentLifes += value;
        }

        public void RecieveDamage(int damage = 1)
        {
            CurrentLifes -= damage;
        }

        public void PickUpItem(Item item)
        {
            try
            {
                Lifepoint lifepoint = (Lifepoint)item;
                CurrentLifes += lifepoint.LifeCount;
            }
            catch
            {
                throw new System.InvalidCastException("Not a lifepoint");
            }
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var damaging = collision.gameObject.GetComponent<DamagingBehaviour>();
            if (damaging != null)
            {
                RaiseRecieveDamageEvent(damaging.Damage);
                RaiseReturnToCheckpointEvent();
            }
        }

        private void RaiseRecieveDamageEvent(int damage)
        {
            EventsHandler.RaiseEvent<IHealthChangeHandler>(h => h.RecieveDamage(damage));
        }

        private void RaiseReturnToCheckpointEvent()
        {
            EventsHandler.RaiseEvent<IReturnToCheckpointHandler>(h => h.ReturnToCheckpoint(CheckpointsHandler.GetLastCheckpoint()));
        }


    }

}
