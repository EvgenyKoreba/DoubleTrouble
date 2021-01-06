using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int lives;
    [SerializeField] private Vector2 respawnPos;
    [SerializeField] private Rigidbody2D rb;


    private void Start()
    {
        SetRespawnPos(new Vector2(transform.position.x, transform.position.y));
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DamageObject"))
            StartCoroutine(TakeDamage());

    }

    private IEnumerator TakeDamage()
    {
        lives--;
        transform.position = respawnPos;
        rb.bodyType = RigidbodyType2D.Static;
        yield return new WaitForSeconds(2f);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
    public void SetRespawnPos(Vector2 respawnPos)
    {
        this.respawnPos = respawnPos;
    }
}
