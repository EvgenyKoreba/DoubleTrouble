using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface IHealthChangeHandler : IGlobalSubscriber
    {
        void Heal(int value = 1);
        void RecieveDamage(int damage = 1);
    }

}
