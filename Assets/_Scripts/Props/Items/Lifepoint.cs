using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

public class Lifepoint : Item
{
    [Header("Set in Inspector: Lifepoint")]
    [SerializeField] private int _lifeCount = 1;

    public int LifeCount => _lifeCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Route<PlayerHealth>(collision);
    }
}
