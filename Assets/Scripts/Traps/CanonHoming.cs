using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHoming : Canon
{
    private Animator homingCanonAnimator;
    private void Start()
    {
        homingCanonAnimator = gameObject.GetComponent<Animator>();
        rotZ = transform.rotation.z;
        isShoting = false;
    }
    private void OnTriggerStay2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            targetPosX = Player.gameObject.transform.position.x;
            targetPosY = Player.gameObject.transform.position.y;
            AimTo();

        }
    }
    private void OnTriggerExit2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            isShoting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            isShoting = true;
            ShootingLoop();
        }
    }
    protected override void ShotAnimation()
    {
        homingCanonAnimator.Play("HomingCanonShot");
    }


}
