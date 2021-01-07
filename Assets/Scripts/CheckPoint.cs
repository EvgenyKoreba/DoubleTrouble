using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    [SerializeField] private bool       isAchived = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStatus ps = collision.gameObject.GetComponent<PlayerStatus>();
        if (ps != null && !isAchived)
            Achiev(ps);
    }


    private void Achiev(PlayerStatus player)
    {
        isAchived = true;
        player.SetRespawnPos(new Vector2(transform.position.x, transform.position.y));
    }

}
