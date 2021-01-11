using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHoming : Canon
{
    [Header("Set Dynamically")]
    [SerializeField] private float rotateSpeed = 100f;
    private Vector2 shootingDirection;

    protected override void Awake()
    {
        base.Awake();
        isShoting = false;
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {

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


    protected override IEnumerator AimToTarget()
    {
        while (true)
        {
            float x = targetPos.x - transform.position.x;
            float y = targetPos.y - transform.position.y;
            float rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;


            Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
            Quaternion RotationInTime = Quaternion.RotateTowards(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            transform.rotation = RotationInTime;

            yield return null;
        }
    }
    private void Start()
    {

    }

}
