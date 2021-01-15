using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    [Header("Canon Settings"), Space(10)]
    [Header("Set in Inspector: Canon")]
    [SerializeField] protected float shootingSpeed;
    [SerializeField] protected float shotForce;
    [SerializeField] private Transform target;


    [Header("Projectile Settings"), Space(10)]
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected float projectileGravityScale;
    [SerializeField] protected float bounciness;
    [SerializeField] protected float lifeTime;


    [Header("Set Dynamically: Canon")]
    [SerializeField] protected bool _isShoting = false;


    protected Animator animator;

    public virtual bool isShoting
    {
        get => _isShoting;
        set 
        { 
            _isShoting = value;
            if (isShoting)
            {
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
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        StartShooting();
    }


    public void StartShooting()
    {
        isShoting = true;
    }


    public void StopShooting()
    {
        isShoting = false;
    }


    protected virtual IEnumerator ShootingLoop()
    {
        while (true)
        {
            ShotAnimation();
            Projectile shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
            shotRB.gravityScale = projectileGravityScale;

            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();
            shotRB.AddForce(direction * shotForce);


            shot.ProjectileSettings(bounciness , lifeTime);

            yield return new WaitForSeconds(1 / shootingSpeed);
        }
    }


    protected virtual void ShotAnimation()
    {
        animator.Play("CanonShot");
    }


    protected virtual void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(target.position.x,
            target.position.y, transform.position.z));
    }

}
