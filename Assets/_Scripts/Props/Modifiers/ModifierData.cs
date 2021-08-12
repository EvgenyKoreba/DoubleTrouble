using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierData : ScriptableObject
{
    [Header("Set in Inspector: Modifier")]
    [SerializeField] private int _maxNumbersOfActivations = 1;
    [SerializeField] private bool _availableOnGround = true;
    [SerializeField] private bool _availableOnAir = true;

    [Header("Set Dynamically: Modifier")]
    [SerializeField] private bool _isActive = false;
    [SerializeField] private int _currentNumbersOfActivations = 1;
}
