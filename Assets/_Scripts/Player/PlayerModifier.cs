using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomEventSystem;

namespace Project.Player
{

    public class PlayerModifier : MonoBehaviour, IPickUpModifierHandler
    {
        [Header("Set in Inspector")]
        [SerializeField] private GroundChecker _groundChecker;

        [Header("Set Dynamically")]
        [SerializeField] private Modifier _currentModifier;

        public Modifier CurrentModifier
        {
            get => _currentModifier;
        }

        #region Events
        private void OnEnable()
        {
            EventsHandler.Subscribe(this);
        }

        private void OnDisable()
        {
            EventsHandler.Unsubscribe(this);
        }

        public void ModifierPickUped(Modifier modifier)
        {
            _currentModifier = modifier;
        }
        #endregion

        public void TryActivateModifier()
        {
            if (_currentModifier != null)
            {
                _currentModifier.ActivationAttempt(_groundChecker.IsGrounded);
            }
        }

        public void DisableModifier()
        {
            _currentModifier.Disable();
        }

        public int GetModifierParamHash()
        {
            return _currentModifier.ParamHash;
        }
    }

}
