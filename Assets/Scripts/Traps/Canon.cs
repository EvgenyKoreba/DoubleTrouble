using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] protected float shootingSpeed;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected Vector3 targetPos;
    [SerializeField] protected float projectileGravityScale;
    [SerializeField] protected float shotForce;
    [SerializeField] protected float projectileLifetime;
    [SerializeField] private bool _isShoting = false;

    [Header("Projectile Settings")]
    [SerializeField] protected float bounciness;
    [SerializeField] protected float lifeTime;


    protected Animator animator;
    private float rotZ;
    private Transform shootDir;

    public bool isShoting
    {
        get { return _isShoting; }
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


    protected virtual void Awake()
    {
        shootDir = gameObject.transform.GetChild(0);
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        StartShooting();
        print(this.transform.up + transform.position);
        print(transform.position);
    }


    public void StartShooting()
    {
        isShoting = true;
    }

    public void StopShooting()
    {
        isShoting = false;
    }

    protected virtual IEnumerator AimToTarget()
    {
        float x = targetPos.x - transform.position.x;
        float y = targetPos.y - transform.position.y;
        rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg + 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ));
        yield return null;
    }

    protected IEnumerator ShootingLoop()
    {
        while (true)
        {
            ShotAnimation();
            Projectile shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
            shotRB.gravityScale = projectileGravityScale;

            Vector2 direction = shootDir.transform.position - transform.position;
            direction.Normalize();
            shotRB.AddForce(direction * shotForce);


            shot.ProjectileSettings(bounciness , lifeTime);

            yield return new WaitForSeconds(1 / shootingSpeed);
        }
    }
    


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(targetPos.x, targetPos.y, transform.position.z));
    }




    protected virtual void ShotAnimation()
    {
        animator.Play("CanonShot");
    }


}
