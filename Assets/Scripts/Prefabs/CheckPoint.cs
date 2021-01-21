using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
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
            Reached(player);
    }


    private void Reached(Player player)
    {
        isReached = true;
        player.SetRespawnPos(transform.position);
        GUI.Button(new Rect(10, 10, 150, 150), "I am a button");
    }



    private void OnEnable()
    {
        EventManager.Subscribe(EVENT_TYPE.CheckpointReached, Test);
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(EVENT_TYPE.CheckpointReached, Test);
    }

    private void Test(object[] pars)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        EventManager.PostNotification(EVENT_TYPE.CheckpointReached, this);
    }
}
