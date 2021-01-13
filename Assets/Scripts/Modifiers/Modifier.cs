using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier: MonoBehaviour
{
    [Header("Set in Inspector: Modifier")]
    public KeyCode useButton;


    protected Player player;


    protected virtual void Awake()
    {
        player = FindObjectOfType<Player>();
    }


    public virtual void Activate() { }


    public virtual void Disable()
    {
        StopAllCoroutines();
    }
}
