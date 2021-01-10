using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("Set Dynamically")]
    [SerializeField] private bool       isAchived = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && !isAchived)
            Achiev(player);
    }


    private void Achiev(Player player)
    {
        isAchived = true;
        player.SetRespawnPos(transform.position);
    }


    //private void Start()
    //{
    //    EventManager.PostNotification(EVENT_TYPE.CheckpointReached, new GameObject());
    //    EventManager.PostNotification(EVENT_TYPE.CheckpointReached);
    //}


    //private void OnEnable()
    //{
    //    EventManager.Subscribe(EVENT_TYPE.CheckpointReached, Test);
    //}


    //private void OnDisable()
    //{
    //    EventManager.Unsubscribe(EVENT_TYPE.CheckpointReached, Test);
    //}

    //private void Test(object[] pars)
    //{

    //}
}
