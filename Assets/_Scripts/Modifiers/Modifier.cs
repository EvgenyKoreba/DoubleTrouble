using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;


public class Modifier: MonoBehaviour, IGlobalSubscriber
{
    #region Fields
    [Header("Set in Inspector: Modifier")]
    [SerializeField] private KeyCode _useButton = KeyCode.Q;
    [SerializeField] private bool _availableOnGround = true;
    [SerializeField] private bool _availableOnAir = true;
    [SerializeField] private int _maxNumbersOfActivations = 1;

    [Header("Set Dynamically: Modifier")]
    private bool _isActive = false;
    private int _currentNumbersOfActivations = 1;

    protected PlayerJumpAggregator Player;
    #endregion

    public bool IsActive
    {
        get => _isActive;
        private set => _isActive = value;
    }

    public KeyCode UseButton => _useButton;

    protected bool IsUseButtonReleased() => Input.GetKeyUp(UseButton);


    public void TryActivate(bool playerIsGrounded)
    {
        if (CanBeActivated(playerIsGrounded))
        {
            Activate();
        }
    }

    private bool CanBeActivated(bool playerIsGrounded)
    {
        return _currentNumbersOfActivations > 0 &&
            (_availableOnGround == playerIsGrounded || _availableOnAir == !playerIsGrounded);
    }

    protected virtual void Activate() 
    {
        _isActive = true;
        _currentNumbersOfActivations--;
    }

    public virtual void Disable()
    {
        _isActive = false;
        Player.ResetJumps();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerJumpAggregator>() != null)
        {
            Player = collision.gameObject.GetComponent<PlayerJumpAggregator>();
            RaisePickUpSelf();
            HideSelf();
        }
    }

    private void RaisePickUpSelf()
    {
        EventsHandler.RaiseEvent<IPickUpModifierHandler>(h => h.ModifierPickUped(this));
    }

    private void HideSelf()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    public void ThrowOut()
    {
        Destroy(gameObject);
    }

    public void Reset()
    {
        _currentNumbersOfActivations = _maxNumbersOfActivations;
    }

    
    protected void OnEnable() => EventsHandler.Subscribe(this);

    protected void OnDisable() => EventsHandler.Unsubscribe(this);
}
