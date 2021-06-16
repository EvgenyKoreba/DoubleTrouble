using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface ILevelFinishedHandler : IGlobalSubscriber
    {
        void LevelFinished();
    }

}
