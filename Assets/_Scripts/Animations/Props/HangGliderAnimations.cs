using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

public class HangGliderAnimations : MonoBehaviour, IPickUpModifierHandler
{
    [Header("Set in Inspector")]
    [SerializeField] private ParticleSystem _explode;

    private void Awake()
    {
        if (_explode.isPlaying)
        {
            Debug.LogError("Explode particles playing default");
        }
    }


    public void ModifierPickUped(Modifier modifier)
    {
        Modifier parentModifier = GetComponentInParent<HangGlider>();
        if (parentModifier.Equals(modifier))
        {
            _explode.Play();
        }
    }

    private void OnEnable() => EventsHandler.Subscribe(this);

    private void OnDisable() => EventsHandler.Unsubscribe(this);
}
