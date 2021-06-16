using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DamagingBehaviour
{
    [SerializeField] private Rigidbody2D _projectileMat;
    private float _lifeTime;
    private bool _isExploding;

    
    public void Start()
    {
        Destroy(gameObject, _lifeTime);
    }


    public void ProjectileSettings(float bounciness, float lifeTime)
    {
        _projectileMat.sharedMaterial.bounciness = bounciness;
        _lifeTime = lifeTime;
    }

    public void ProjectileSettings(float bounciness, float lifeTime, bool isExploding)
    {
        _projectileMat.sharedMaterial.bounciness = bounciness;
        _lifeTime = lifeTime;
        _isExploding = isExploding;
    }
}
