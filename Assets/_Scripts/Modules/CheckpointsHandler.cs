using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class CheckpointsHandler : MonoBehaviour, ICheckpointReachHandler
{
    #region Singleton
    private static CheckpointsHandler _instance;

    static public CheckpointsHandler INSTANCE
    {
        get { return _instance; }
        private set { _instance = value; }
    }
    #endregion

    #region Fields
    [Header("Set in Inspector")]
    [SerializeField] private float _returnToCheckpointDelay;

    [Header("Set Dynamically")]
    [SerializeField] private Checkpoint _lastCheckpoint;
    #endregion

    #region Getters
    static public float GetReturnToCheckpointDelay() => INSTANCE._returnToCheckpointDelay;

    static public Checkpoint GetLastCheckpoint() => INSTANCE._lastCheckpoint;
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

    public void CheckpointReach(Checkpoint checkpoint)
    {
        INSTANCE._lastCheckpoint = checkpoint;
    }
    #endregion

    private void Awake()
    {
        PrepareSingleton();
        DontDestroyOnLoad(gameObject);
    }

    private void PrepareSingleton()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(INSTANCE.gameObject);
            INSTANCE = this;
        }
    }
}
