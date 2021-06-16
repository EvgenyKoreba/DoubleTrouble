using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface ICheckpointReachHandler : IGlobalSubscriber
    {
        void CheckpointReach(Checkpoint checkpoint);
    }

}
