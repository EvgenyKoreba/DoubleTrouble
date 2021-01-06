using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonHoming : MonoBehaviour
{

    [SerializeField] private float timeForNextShoot;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float targetPosX;
    [SerializeField] private float targetPosY;
    [SerializeField] private float projectileUnitsPerSecond;
    [SerializeField] private float shootingRange;

    private float rotZ;
    void Start()
    {
        rotZ = transform.rotation.z;
        Invoke("ShootingLoop", timeForNextShoot);
    }
    private void ShootingLoop()
    {
        GameObject shot = Instantiate(projectile, transform.position, Quaternion.identity);
        shot.GetComponent<ObjectMover>().SetDestination(targetPosX, targetPosY, projectileUnitsPerSecond, true);
        Invoke("ShootingLoop", timeForNextShoot);
    }
    
    private void AimTo()
    {
        float x = targetPosX - transform.position.x;
        float y = targetPosY - transform.position.y;
        rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ + 90));
    }
    private void OnTriggerStay2D(Collider2D Player)
    {
        if (Player.gameObject.tag == "Player")
        {
            Ray2D shootDir = new Ray2D(new Vector2(transform.position.x, transform.position.y), new Vector2(Player.gameObject.transform.position.x, Player.gameObject.transform.position.y));
            targetPosX = shootDir.GetPoint(shootingRange).x;
            targetPosY = shootDir.GetPoint(shootingRange).y;
            AimTo();

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
