using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class Modifier: MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: Modifier")]
    public KeyCode UseButton = KeyCode.Q;
    [SerializeField] private bool _availableOnGround = true;
    [SerializeField] private bool _availableOnAir = true;


    [Header("Set Dynamically: Modifier")]
    private bool _isActive = false;

    protected PlayerJumpAggregator PlayerJumpAggregator;
    #endregion

    #region Properties
    public bool IsActive
    {
        get => _isActive;
        private set => _isActive = value;
    }
    #endregion

    protected virtual void Awake()
    {
        PlayerJumpAggregator = FindObjectOfType<PlayerJumpAggregator>();
    }

    public void TryActivate(bool isGrounded)
    {
        if (_availableOnGround == isGrounded || _availableOnAir == !isGrounded)
        {
            Activate();
        }
    }

    protected virtual void Activate() 
    {
        _isActive = true;
    }

    public virtual void Disable()
    {
        _isActive = false;
        PlayerJumpAggregator.ResetJumps();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            PickUp();
        }
    }

    protected virtual void PickUp()
    {
        EventsHandler.RaiseEvent<IPickUpModifierHandler>(h => h.ModifierPickUped(this));
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public virtual void ThrowOut()
    {
        Destroy(gameObject);
    }
}
