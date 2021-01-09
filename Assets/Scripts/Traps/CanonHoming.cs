using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHoming : Canon
{
    [Header("Set Dynamically")]
    [SerializeField] private float rotateSpeed = 100f;


    protected override void Awake()
    {
        base.Awake();
        isShoting = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {

            //targetPos.x = other.gameObject.transform.position.x;
            //targetPos.y = other.gameObject.transform.position.y;
            //AimToTarget();

            targetPos = other.gameObject.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            StopShooting();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            StartShooting();
        }
    }
    protected override void ShotAnimation()
    {
        animator.Play("HomingCanonShot");
    }


    protected override IEnumerator AimToTarget1()
    {
        while (true)
        {
            float x = targetPos.x - transform.position.x;
            float y = targetPos.y - transform.position.y;
            float rotZ = Mathf.Atan2(y, x);

            // ВОТ ТУТ ИСПРАВИТЬ, ХУЁВО ПОВОРАЧИВАЕТСЯ
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(x,y,0));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
            yield return null;
        }
    }

}
