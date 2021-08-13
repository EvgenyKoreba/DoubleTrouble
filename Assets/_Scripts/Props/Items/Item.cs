using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public abstract class Item : MonoBehaviour
{
    protected void Route<T>(Collider2D container)
    {
        if (container.GetComponent<T>() != null)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        RaisePickUpItemEvent();
        Destroy(gameObject);
    }

    private void RaisePickUpItemEvent()
    {
        EventsHandler.RaiseEvent<IPickUpItem>(h => h.PickUpItem(this));
    }
}
