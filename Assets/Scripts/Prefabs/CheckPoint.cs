using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{

    private enum CheckpointType
    {
        Intermediate,
        Final
    }


    [Header("Set Dynamically")]
    [SerializeField] private bool               isReached = false;
    [SerializeField] private CheckpointType     type = CheckpointType.Intermediate;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReached)
        {
            return;
        } 

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            isReached = true;
            switch (type)
            {
                case CheckpointType.Intermediate:
                    EventManager.PostNotification(EVENT_TYPE.CheckpointReached, this);
                    ShowInsert(InsertsContainer.S.checkpointReach);
                    break;
                case CheckpointType.Final:
                    EventManager.PostNotification(EVENT_TYPE.LevelFinished, this);
                    ShowInsert(InsertsContainer.S.levelFinish);
                    break;
            }
        }
    }


    private void ShowInsert(Canvas insert)
    {
        Instantiate(insert);
    }
}
