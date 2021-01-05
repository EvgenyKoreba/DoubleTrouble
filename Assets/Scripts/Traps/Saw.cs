using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MovingBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float          damage;


    private void Start()
    {
        Move();
    }
}
