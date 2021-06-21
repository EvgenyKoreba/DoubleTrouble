using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class Player : MonoBehaviour, ICheckpointReachHandler, IHealthChangeHandler, IStartLevelHandler, 
    IReturnToCheckpointHandler
{
    #region Fields
    [Header("Set in Inspector")]
    [SerializeField] private int _maxLifes;
    [SerializeField] private float _returnDelay;


    [Header("Set Dynamically")]
    [SerializeField] private int _currentLives;
    #endregion

    #region Properties
    public int CurrentLifes
    {
        get { return _currentLives; }
        private set { 
            if (value <= 0)
            {
                EventsHandler.RaiseEvent<IRestartLevelHandler>(r => r.RestartLevel());
            }

            _currentLives = Mathf.Clamp(value, 1, _maxLifes);
        }
    }

    public int MaxLifes
    {
        get { return _maxLifes; }
        private set { _maxLifes = value; }
    }
    #endregion

    #region Events
    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }

    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }

    public void Heal(int value = 1)
    {
        CurrentLifes += value;
    }

    public void RecieveDamage(int damage = 1)
    {
        CurrentLifes -= damage;
    }

    public void StartLevel(LevelData level)
    {
        CurrentLifes = _maxLifes;
    }

    public void CheckpointReach(Checkpoint checkpoint)
    {

    }
    #endregion

    private void Start()
    {
        CurrentLifes = _maxLifes;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DamagingBehaviour>() != null)
        {
            DamagingBehaviour dB = collision.gameObject.GetComponent<DamagingBehaviour>();
            EventsHandler.RaiseEvent<IHealthChangeHandler>(h => h.RecieveDamage(dB.Damage));
            EventsHandler.RaiseEvent<IReturnToCheckpointHandler>(h => 
                h.ReturnToCheckpoint(CheckpointsHandler.GetLastCheckpoint()));
        }
    }

    public void ReturnToCheckpoint(Checkpoint checkpoint)
    {
        StartCoroutine(RespawnOnCheckpoint(checkpoint));
    }

    private IEnumerator RespawnOnCheckpoint(Checkpoint checkpoint)
    {
        CurrentLifes--;
        transform.position = checkpoint.transform.position;
        yield return null;
        //Time.timeScale = 0;
        //yield return new WaitForSeconds(_returnDelay);
        //Time.timeScale = 1;
    }
}
