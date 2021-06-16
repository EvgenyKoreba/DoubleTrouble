using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace CustomEventSystem
{

    public interface IPickUpItemHandler : IGlobalSubscriber
    {
        void PickUpItem(IPickupableItem item);
    }

}
