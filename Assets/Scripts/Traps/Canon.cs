using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{

    [SerializeField] private float timeForNextShoot;
    [SerializeField] private DamageObject projectile;
    [SerializeField] private float targetPosX;
    [SerializeField] private float targetPosY;

    private float rotZ;
    void Start()
    {
        rotZ = transform.rotation.z;
        AimTo();
        Invoke("ShootingLoop", timeForNextShoot);
    }
    private void ShootingLoop()
    {
        DamageObject shot = Instantiate(projectile , transform.position , Quaternion.identity);
        shot.SetDestination(targetPosX, targetPosY, false);

        Invoke("ShootingLoop", timeForNextShoot);
    }
    private void AimTo()
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

}
