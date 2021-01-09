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


    protected Animator animator;


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
        animator = GetComponent<Animator>();
    }


    private void Start()
    {
        //StartShooting();
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
        float rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
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

            // Надо изменить direction так, чтобы оно зависело от rotZ
            Vector3 direction = targetPos - transform.position;
            direction.Normalize();
            shotRB.AddForce(direction * shotForce);

            // не уверен что пушка должна знать когда уничтожаются снаряды
            // надо как-то передать эту ответственность самому прожектайлу
            Destroy(shot.gameObject, projectileLifetime);

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
