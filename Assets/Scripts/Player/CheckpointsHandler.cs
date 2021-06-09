using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class CheckpointsHandler : MonoBehaviour, ICheckpointReachReturnHandler, IRespawnLevelHandler
{
    [Header("Set in Inspector")]
    [SerializeField] private Checkpoint _levelStartCheckpoint;

    [Header("Set Dynamically")]
    [SerializeField] private Checkpoint _lastCheckpoint;


    private void Awake()
    {
        _lastCheckpoint = _levelStartCheckpoint;
    }


    private void OnEnable()
    {
        EventsHandler.Subscribe(this);
    }


    private void OnDisable()
    {
        EventsHandler.Unsubscribe(this);
    }


    public void HandleCheckpointReach(Checkpoint checkpoint)
    {
        _lastCheckpoint = checkpoint;
    }

    public void HandleReturnToCheckpoint(Checkpoint checkpoint)
    {

    }

    public void RespawnLevel()
    {
        _lastCheckpoint = _levelStartCheckpoint;
    }
}
