using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    #region Fields
    [Header("Canon Settings"), Space(10)]
    [Header("Set in Inspector: Canon")]
    [SerializeField] protected float shootingSpeed;
    [SerializeField] protected float shotForce;
    [SerializeField] private int _amountOfShots;
    [SerializeField] private float _coneOfAffectInDeg;


    [Header("Projectile Settings"), Space(10)]
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected float projectileGravityScale;
    [SerializeField] protected float bounciness;
    [SerializeField] protected float lifeTime;


    [Header("Set Dynamically: Canon")]
    [SerializeField] protected bool _isShoting = false;


    protected Animator animator;
    protected GameObject[] targets;
    #endregion

    #region Properties
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
    #endregion

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TargetsSpawn();
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

    protected void TargetsSpawn()
    {
        float shift = Mathf.Tan(_coneOfAffectInDeg * Mathf.Deg2Rad / 2);
        float step = 0;
        if (_amountOfShots == 1)
        {
            shift = 0;
        }
        else
        {
            step = shift * 2 / (_amountOfShots - 1);

        }

        targets = new GameObject[_amountOfShots];

        for (int i = 0; i < _amountOfShots; i++)
        {
            GameObject target = new GameObject("Target");
            targets[i] = target;
            target.transform.parent = transform;
            targets[i].transform.localPosition = new Vector2(step * i - shift, -1);
        }
    }

    protected virtual IEnumerator ShootingLoop()
    {
        while (true)
        {
            for (int i = 0; i < _amountOfShots; i++)
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

    protected virtual void ShotAnimation()
    {
        animator.Play("CanonShot");
    }

    protected virtual void OnDrawGizmosSelected()
    {
        try
        {
            for (int i = 0; i < _amountOfShots; i++)
            {
                Gizmos.DrawLine(transform.position, new Vector3(targets[i].transform.position.x,
                    targets[i].transform.position.y, transform.position.z));
            }
        }
        catch { }
    }

}
