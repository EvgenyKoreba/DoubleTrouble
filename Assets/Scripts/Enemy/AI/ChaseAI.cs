using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform barrierCheckPosition;
    [SerializeField] private float wallCheckLine;
    [SerializeField] private float holeCheckLine;
    [SerializeField] private float groundCheckRadius;

    private Rigidbody2D rb;
    private bool isCanJump = true;
    private bool isGrounded = true;
    private float jumpCoolDown = 1;

    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {

            StartCoroutine(StartChase(other.gameObject));
        }
    }


    private IEnumerator StartChase(GameObject target)
    {
        int side = 0;




        while (transform.position.x - target.transform.position.x > 0.1f || transform.position.x - target.transform.position.x < 0.1f)
        {
            if (target.transform.position.x - transform.position.x > 0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                side = 1;
            }
            else if (target.transform.position.x - transform.position.x < 0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
                side = -1;
            }
            rb.velocity = new Vector2(speed * side, rb.velocity.y);

            yield return new WaitForFixedUpdate();

        }
    }

    private bool IsFacingBarrier()
    {
        Vector2 wallCheckVector = new Vector2(barrierCheckPosition.position.x + wallCheckLine, barrierCheckPosition.position.y);
        Vector2 holeCheckVector = new Vector2(barrierCheckPosition.position.x, barrierCheckPosition.position.y - holeCheckLine);

        if (Physics2D.Linecast(barrierCheckPosition.position, wallCheckVector, ground))
        {
            return true;
        }

        else if (!Physics2D.Linecast(barrierCheckPosition.position, holeCheckVector, ground))
        {
            return true;
        }
        else
            return false;

    }

    private void Update()
    {
        if (IsFacingBarrier())
        {
            StartCoroutine(Jump());
        }

        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, ground);
    }

    private IEnumerator Jump()
    {
        if (isCanJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isCanJump = false;
            yield return new WaitForSeconds(jumpCoolDown);
            isCanJump = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(barrierCheckPosition.position, new Vector2(barrierCheckPosition.position.x + wallCheckLine, barrierCheckPosition.position.y));
        Gizmos.DrawLine(barrierCheckPosition.position, new Vector2(barrierCheckPosition.position.x, barrierCheckPosition.position.y - holeCheckLine));
        Gizmos.DrawWireSphere(transform.position, groundCheckRadius);
    }





}
