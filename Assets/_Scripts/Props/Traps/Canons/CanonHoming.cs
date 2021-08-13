using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

public class CanonHoming : Canon
{
    #region Fields
    [Header("Set in Inspector: CanonHoming")]
    [SerializeField] protected Vector3 targetPos;

    [Header("Set Dynamically: CanonHoming"), Space(10)]
    [SerializeField] private float _rotateSpeed = 100f;
    #endregion

    #region Properties
    public override bool isShoting
    {
        get => base.isShoting;
        set
        {
            _isShoting = value;
            if (isShoting)
            {
                StartCoroutine(AimToTarget());
                StartCoroutine(ShootingLoop());
            }
            else
            {
                StopAllCoroutines();
            }
        }
    }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        isShoting = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCollector>() != null)
        {

            targetPos = other.gameObject.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCollector>() != null)
        {
            StopShooting();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCollector>() != null)
        {
            StartShooting();
        }
    }

    protected override void ShotAnimation()
    {
        animator.Play("HomingCanonShot");
    }

    private IEnumerator AimToTarget()
    {
        while (true)
        {
            float x = targetPos.x - transform.position.x;
            float y = targetPos.y - transform.position.y;
            float rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;


            Quaternion newRotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
            Quaternion RotationInTime = Quaternion.RotateTowards(transform.rotation, newRotation, _rotateSpeed * Time.deltaTime);
            transform.rotation = RotationInTime;

            yield return null;
        }
    }

    private void Start()
    {
        TargetsSpawn();
    }

}
