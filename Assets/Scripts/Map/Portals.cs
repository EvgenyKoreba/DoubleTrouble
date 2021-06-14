using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    [SerializeField] GameObject portalBlue;
    [SerializeField] GameObject portalOrange;
    [SerializeField] float teleportRecharge;

    private GameObject player;
    private Collider2D playerCollider;
    private Collider2D portalBlueCollider;
    private Collider2D portalOrangeCollider;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>().gameObject;
        playerCollider = player.GetComponent<Collider2D>();


        portalBlueCollider = portalBlue.GetComponent<Collider2D>();
        portalOrangeCollider = portalOrange.GetComponent<Collider2D>();


        StartCoroutine(Teleport());
    }


    private IEnumerator Teleport()
    {
        while (true)
        {
            if (portalOrangeCollider.IsTouching(playerCollider))
            {
                player.transform.position = portalBlue.transform.position;
                yield return new WaitForSeconds(teleportRecharge);
            }
            else if (portalBlueCollider.IsTouching(playerCollider))
            {
                player.transform.position = portalOrange.transform.position;
                yield return new WaitForSeconds(teleportRecharge);
            }
            yield return new WaitForFixedUpdate();
        }



    }
}
