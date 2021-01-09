using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private int maxLives;
    [SerializeField] private Vector2 respawnPos;
    [SerializeField] private Rigidbody2D rb;


    [Header("Set Dynamically")]
    [SerializeField] private int _currentLives;


    private void Awake()
    {
        currentLives = maxLives;
    }


    private void Start()
    {
        SetRespawnPos(new Vector2(transform.position.x, transform.position.y));
        rb = GetComponent<Rigidbody2D>();
    }


    private int currentLives
    {
        get { return _currentLives; }
        set { _currentLives = value; }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("DamageObject"))
    //        StartCoroutine(TakeDamage());
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DamageObject"))
            StartCoroutine(TakeDamage());
    }


    private IEnumerator TakeDamage()
    {
        currentLives--;
        transform.position = respawnPos;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(2f);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }


    public void SetRespawnPos(Vector3 respawnPos)
    {
        this.respawnPos = respawnPos;
    }
}
