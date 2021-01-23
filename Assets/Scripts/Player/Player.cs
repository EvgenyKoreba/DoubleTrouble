using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private int maxLives;
    [SerializeField] private Checkpoint lastCheckpoint;
    [SerializeField] private float respawnDelay;


    [Header("Set Dynamically")]
    [SerializeField] private int _currentLives;


    private Rigidbody2D _rigidBody;


    public int currentLives
    {
        get { return _currentLives; }
        set { 
            if (value < 0)
            {
                Die();
            }

            _currentLives = Mathf.Clamp(value, 1, maxLives);
        }
    }


    private void Awake()
    {
        currentLives = maxLives;
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        //SetRespawnPos(new Vector2(transform.position.x, transform.position.y));
    }


    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("DamageObject"))
    //        StartCoroutine(TakeDamage());
    //}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DamageObject"))
            StartCoroutine(RespawnOnCheckpoint());
    }


    //private IEnumerator TakeDamage()
    //{
    //    currentLives--;
    //    transform.position = respawnPos;
    //    _rigidBody.bodyType = RigidbodyType2D.Static;
    //    yield return new WaitForSeconds(2f);
    //    _rigidBody.bodyType = RigidbodyType2D.Dynamic;
    //}


    private IEnumerator RespawnOnCheckpoint()
    {
        currentLives--;
        transform.position = lastCheckpoint.transform.position;
        Time.timeScale = 0;
        yield return new WaitForSeconds(respawnDelay);
        Time.timeScale = 1;
    }


    private void Die()
    {

    }


    private void OnEnable()
    {
        EventManager.Subscribe(EVENT_TYPE.CheckpointReached, ReachCheckpoint);
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(EVENT_TYPE.CheckpointReached, ReachCheckpoint);
    }


    private void ReachCheckpoint(object[] parameters)
    {
        try
        {
            Checkpoint newCP = (Checkpoint)parameters[0];
            lastCheckpoint = newCP;
        }
        catch (System.InvalidCastException)
        {
            throw new System.Exception("Sended not a checkpoint");
        }
    }
}
