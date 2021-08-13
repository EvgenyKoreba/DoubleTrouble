using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace Project.Player
{

    [RequireComponent(typeof(PlayerPhysics))]
    public class PlayerCollector : MonoBehaviour, ICheckpointReachHandler
    {
        #region Fields
        [Header("Set in Inspector")]
        [SerializeField] private GroundChecker _groundChecker;
        [SerializeField] private float _returnDelay;


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

        public void CheckpointReach(Checkpoint checkpoint)
        {

        }

        public void ReturnToCheckpoint(Checkpoint checkpoint)
        {
            StartCoroutine(RespawnOnCheckpoint(checkpoint));
        }

        private IEnumerator RespawnOnCheckpoint(Checkpoint checkpoint)
        {
            transform.position = checkpoint.transform.position;
            yield return null;
            //yield return new WaitForSeconds(_returnDelay);
        }
        #endregion

    }

}
