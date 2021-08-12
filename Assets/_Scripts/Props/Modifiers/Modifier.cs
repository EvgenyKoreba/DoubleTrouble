using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;
using Project.Animations;

[RequireComponent(typeof(Renderer), typeof(Collider2D))]
public class Modifier : MonoBehaviour, IGlobalSubscriber
{
    #region Fields
    [Header("Set in Inspector: Modifier")]
    [SerializeField] private int _maxNumbersOfActivations = 1;
    [SerializeField] private bool _availableOnGround = true;
    [SerializeField] private bool _availableOnAir = true;

    [Header("Set Dynamically: Modifier")]
    [SerializeField] private bool _isActive = false;
    [SerializeField] private int _currentNumbersOfActivations = 1;

    protected PlayerPhysics PlayerPhysics;
    protected string AnimatorParameter = "";

    private int _parameterHash;
    #endregion

    public bool IsActive
    {
        get => _isActive;
        private set => _isActive = value;
    }

    public int ParamHash { get => _parameterHash; }

    public int CurrentNumberOfActivations
    {
        get => _currentNumbersOfActivations;
        set {
            _currentNumbersOfActivations = value;
            if (_currentNumbersOfActivations == 0)
            {
                ThrowOut();
            }
        }
    }

    private void ThrowOut()
    {
        Destroy(gameObject);
    }

    private void Start()
    {
        ResetActivations();
        GetParameterHash();
    }

    public void ResetActivations()
    {
        _currentNumbersOfActivations = _maxNumbersOfActivations;
    }

    private void GetParameterHash()
    {
        _parameterHash = AnimatorParametersCustomizer.GetHash(AnimatorParameter);
    }

    public void ActivationAttempt(bool playerIsGrounded)
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
    }

    public virtual void Disable()
    {
        _isActive = false;
        _currentNumbersOfActivations--;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        var physicsComponent = collision.gameObject.GetComponentInParent<PlayerPhysics>();
        if (physicsComponent != null)
        {
            PlayerPhysics = physicsComponent;
            RaisePickUpEvent();
            HideSelf();
        }
    }

    private void RaisePickUpEvent()
    {
        EventsHandler.RaiseEvent<IPickUpModifierHandler>(h => h.ModifierPickUped(this));
    }

    private void HideSelf()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.enabled = false;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;
    }

    protected void OnEnable() => EventsHandler.Subscribe(this);

    protected void OnDisable() => EventsHandler.Unsubscribe(this);
}
