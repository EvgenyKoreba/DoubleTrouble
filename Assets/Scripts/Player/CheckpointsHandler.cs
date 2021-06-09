using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class CheckpointsHandler : MonoBehaviour, ICheckpointReachReturnHandler, IRespawnLevelHandler
{
    [Header("Set in Inspector")]
    [SerializeField] private Checkpoint _levelStartCheckpoint;

    [Header("Set Dynamically")]
    public Checkpoint LastCheckpoint;


    private void Awake()
    {
        LastCheckpoint = _levelStartCheckpoint;
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
        LastCheckpoint = checkpoint;
    }

    public void HandleReturnToCheckpoint(Checkpoint checkpoint)
    {

    }

    public void RespawnLevel()
    {
        LastCheckpoint = _levelStartCheckpoint;
    }
}
