using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier: MonoBehaviour
{
    [Header("Set in Inspector: Modifier")]
    public KeyCode useButton;


    protected PlayerJumpAgregator player;


    protected virtual void Awake()
    {
        player = FindObjectOfType<PlayerJumpAgregator>();
    }


    public virtual void Activate() { }


    public virtual void Disable()
    {
        StopAllCoroutines();
    }
}
