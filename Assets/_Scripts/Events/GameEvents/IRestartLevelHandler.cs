using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface IRestartLevelHandler : IGlobalSubscriber
    {
        void RestartLevel();
    }

}
