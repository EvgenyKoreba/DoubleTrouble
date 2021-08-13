using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Player;

public class MultiJump : Item
{
    [Header("Set in Inspector: MultiJump")]
    [SerializeField] private int _jumpCount = 1;

    public int JumpCount => _jumpCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Route<PlayerMidAirAggregator>(collision);
    }

}
