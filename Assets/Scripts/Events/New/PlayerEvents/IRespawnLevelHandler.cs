using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public interface IRespawnLevelHandler : IGlobalSubscriber
{
    void RespawnLevel();
}
