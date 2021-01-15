using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D projectileMat;
    private float lifeTime;
    private bool isExploding;

    
    public void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    public void ProjectileSettings(float bounciness, float lifeTime)
    {
        projectileMat.sharedMaterial.bounciness = bounciness;
        this.lifeTime = lifeTime;
    }

    public void ProjectileSettings(float bounciness, float lifeTime, bool isExploding)
    {
        projectileMat.sharedMaterial.bounciness = bounciness;
        this.lifeTime = lifeTime;
        this.isExploding = isExploding;
    }
}
