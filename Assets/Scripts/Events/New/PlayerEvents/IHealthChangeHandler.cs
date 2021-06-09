using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public interface IHealthChangeHandler : IGlobalSubscriber
{ 
    void HandleHealing(int value = 1);
    void HandleRecieveDamage(int damage = 1);
}
