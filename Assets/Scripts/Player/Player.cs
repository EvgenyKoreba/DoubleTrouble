using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class Player : MonoBehaviour, ICheckpointReachReturnHandler, IHealthChangeHandler, IRespawnLevelHandler
{
    [Header("Set in Inspector")]
    public int _maxLifes;
    [SerializeField] private float _returnDelay;


    [Header("Set Dynamically")]
    [SerializeField] private int _currentLives;


    public int currentLifes
    {
        get { return _currentLives; }
        private set { 
            if (value <= 0)
            {
                EventsHandler.RaiseEvent<IRespawnLevelHandler>(r => r.RespawnLevel());
            }

            _currentLives = Mathf.Clamp(value, 1, _maxLifes);
        }
    }


    private void Start()
    {
        currentLifes = _maxLifes;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DamagingBehaviour>() != null)
        {
            DamagingBehaviour dB = collision.gameObject.GetComponent<DamagingBehaviour>();
            EventsHandler.RaiseEvent<IHealthChangeHandler>(h => h.HandleRecieveDamage(dB.damage));
        }
    }


    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }


    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }


    public void HandleHealing(int value = 1)
    {
        currentLifes += value;
    }

    public void HandleRecieveDamage(int damage = 1)
    {
        currentLifes -= damage;
    }

    public void RespawnLevel()
    {
        currentLifes = _maxLifes;
    }


    public void HandleCheckpointReach(Checkpoint checkpoint)
    {

    }

    public void HandleReturnToCheckpoint(Checkpoint checkpoint)
    {
        StartCoroutine(RespawnOnCheckpoint(checkpoint));
    }

    private IEnumerator RespawnOnCheckpoint(Checkpoint checkpoint)
    {
        currentLifes--;
        transform.position = checkpoint.transform.position;
        Time.timeScale = 0;
        yield return new WaitForSeconds(_returnDelay);
        Time.timeScale = 1;
    }
}
