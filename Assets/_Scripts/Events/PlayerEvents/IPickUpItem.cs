using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface IPickUpItem : IGlobalSubscriber
    {
        void PickUpItem(Item item);
    }

}
