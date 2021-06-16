using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface IReturnToCheckpointHandler : IGlobalSubscriber
    {
        void ReturnToCheckpoint(Checkpoint checkpoint);
    }

}
