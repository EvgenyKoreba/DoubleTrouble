using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonMultiplied : Canon
{
    [Header("Multiplied Canon Settings")]
    [SerializeField] private int amountOfShots;
    [SerializeField] private float coneOfAffectInDeg;

    private GameObject[] targets;
    private void Start()
    {
        TargetsSpawn();
        isShoting = true;
    }
    private void TargetsSpawn()
    {
        float shift = Mathf.Tan(coneOfAffectInDeg * Mathf.Deg2Rad / 2);
        float step = shift * 2 / (amountOfShots - 1);

        float iterationVar = 0;
        targets = new GameObject[amountOfShots];

        for (int i = 0; i < amountOfShots; i++)
        {
            targets[i] = Instantiate(new GameObject("Target"), transform);
            targets[i].transform.localPosition = new Vector2(iterationVar - shift, -1);
            iterationVar += step;
        }
    }
    protected override IEnumerator ShootingLoop()
    {
        while (true)
        {
            for (int i = 0; i < amountOfShots; i++)
            {
                ShotAnimation();
                Projectile shot = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Rigidbody2D shotRB = shot.GetComponent<Rigidbody2D>();
                shotRB.gravityScale = projectileGravityScale;

                Vector2 direction = targets[i].transform.position - transform.position;
                direction.Normalize();
                shotRB.AddForce(direction * shotForce);


                shot.ProjectileSettings(bounciness, lifeTime);
            }

            yield return new WaitForSeconds(1 / shootingSpeed);
        }
    }




    protected override void OnDrawGizmosSelected()
    {
        try
        {
            for (int i = 0; i < amountOfShots; i++)
            {
                Gizmos.DrawLine(transform.position, new Vector3(targets[i].transform.position.x,
                    targets[i].transform.position.y, transform.position.z));
            }
        }
        catch  { };
    }

}
