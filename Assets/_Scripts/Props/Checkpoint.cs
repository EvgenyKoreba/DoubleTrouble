using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CustomEventSystem;

public class Checkpoint : MonoBehaviour
{
    private enum CheckpointType
    {
        Intermediate,
        Final
    }


    [Header("Set Dynamically")]
    [SerializeField] private bool               _isReached = false;
    [SerializeField] private CheckpointType     _type = CheckpointType.Intermediate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isReached)
        {
            return;
        } 

        PlayerCollector player = collision.gameObject.GetComponent<PlayerCollector>();
        if (player != null)
        {
            _isReached = true;
            switch (_type)
            {
                case CheckpointType.Intermediate:
                    EventsHandler.RaiseEvent<ICheckpointReachHandler>(h => h.CheckpointReach(this));
                    ShowInsert(InsertsContainer.INSTANCE.checkpointReach);
                    break;
                case CheckpointType.Final:
                    EventsHandler.RaiseEvent<ILevelFinishedHandler>(h => h.LevelFinished());
                    ShowInsert(InsertsContainer.INSTANCE.levelFinish);
                    break;
            }
        }
    }


    private void ShowInsert(Canvas insert)
    {
        Instantiate(insert);
    }
}
