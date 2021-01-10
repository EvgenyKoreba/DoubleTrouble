using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Parachute : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] private float lifeTime = 5f;
    [SerializeField] public float gravityReductionFactor = 0.1f;


    [HideInInspector] public Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    public void StartPlanning()
    {

    }
}
