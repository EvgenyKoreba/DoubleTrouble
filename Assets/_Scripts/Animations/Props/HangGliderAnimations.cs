using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace Project.Animations
{

    public class HangGliderAnimations : MonoBehaviour, IPickUpModifierHandler
    {
        [Header("Set in Inspector")]
        [SerializeField] private ParticleSystem _explodePrefab;

        public void ModifierPickUped(Modifier modifier)
        {
            Modifier parentModifier = GetComponentInParent<HangGlider>();
            if (parentModifier.Equals(modifier))
            {
                ParticleSystem explode = Instantiate(_explodePrefab, transform);
                explode.Play();
            }
        }

        private void OnEnable() => EventsHandler.Subscribe(this);

        private void OnDisable() => EventsHandler.Unsubscribe(this);
    }

}
