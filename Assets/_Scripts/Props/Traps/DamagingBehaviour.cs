using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public abstract class DamagingBehaviour : MonoBehaviour
{
    [Header("Set in Inspector: DamagingBehaviour")]
    [SerializeField] private int _damage = 1;

    public int Damage { get => _damage; }
}
