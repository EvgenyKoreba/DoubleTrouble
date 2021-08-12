using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class MultiJump : MonoBehaviour, IPickupableItem
{
    #region Fields
    [Header("Set in Inspector: MultiJump")]
    [SerializeField] private int _jumpCount = 1;
    #endregion

    #region Properties
    public int JumpCount => _jumpCount;
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var midAirComponent = collision.gameObject.GetComponentInParent<PlayerMidAirAggregator>();
        if (midAirComponent != null)    
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        EventsHandler.RaiseEvent<IPickUpItem>(h => h.PickUpItem(this));
        Destroy(gameObject);
    }

}
