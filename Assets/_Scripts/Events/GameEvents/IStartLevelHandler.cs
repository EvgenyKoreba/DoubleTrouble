using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{
    public interface IStartLevelHandler : IGlobalSubscriber
    {
        void StartLevel(LevelData level);
    }
}
