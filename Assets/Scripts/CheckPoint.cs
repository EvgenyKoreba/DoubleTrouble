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

}
