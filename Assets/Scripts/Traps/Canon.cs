using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] protected float timeForNextShoot;
    [SerializeField] protected Projectile projectile;
    [SerializeField] protected float targetPosX;
    [SerializeField] protected float targetPosY;
    [SerializeField] protected float shotGravityScale;
    [SerializeField] protected float shotForce;
    [SerializeField] protected float timeBeforeShotDestroyed;
    [SerializeField] protected bool isShoting = true;

    protected float rotZ;
    protected Vector2 target;
    private Animator canonAnimator;
    private void Start()
    {
        canonAnimator = gameObject.GetComponent<Animator>();
        rotZ = transform.rotation.z;
        AimTo();
        Invoke("ShootingLoop", timeForNextShoot);
    }
    protected void ShootingLoop()
    {
        ShotAnimation();
        Projectile shot = Instantiate(projectile, transform.position, Quaternion.identity);
        Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
        shotRB.gravityScale = shotGravityScale;
        target = new Vector2(targetPosX - transform.position.x, targetPosY - transform.position.y);
        target.Normalize();
        shotRB.AddForce(target * shotForce);
        if (isShoting)
            Invoke("ShootingLoop", timeForNextShoot);

        Destroy(shot.gameObject, timeBeforeShotDestroyed);
    }
    protected void AimTo()
    {
        float x = targetPosX - transform.position.x;
        float y = targetPosY - transform.position.y;
        rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(targetPosX, targetPosY, transform.position.z));
    }
    protected virtual void ShotAnimation()
    {
        canonAnimator.Play("CanonShot");
    }


}
