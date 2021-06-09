using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public interface ICheckpointReachReturnHandler : IGlobalSubscriber
{ 
    void HandleCheckpointReach(Checkpoint checkpoint);
    void HandleReturnToCheckpoint(Checkpoint checkpoint);
}
