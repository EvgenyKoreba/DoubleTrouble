using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    private bool isAchived = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerStatus>() != null && !isAchived)
            CheckPointAchived(collision.gameObject.GetComponent<PlayerStatus>());
    }


    private void CheckPointAchived(PlayerStatus player)
    {
        print("CheckPoint achived");
        isAchived = true;
        player.SetRespawnPos(new Vector2(transform.position.x, transform.position.y));
    }

}
