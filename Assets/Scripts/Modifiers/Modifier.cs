using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier: MonoBehaviour
{
    [Header("Set in Inspector: Modifier")]
    public KeyCode useButton;


    [Header("Set Dynamically: Modifier")]
    public bool isActive = false;


    protected PlayerJumpAgregator playerJumpAgregator;


    protected virtual void Awake()
    {
        playerJumpAgregator = FindObjectOfType<PlayerJumpAgregator>();
    }


    public virtual void Activate() 
    {
        isActive = true;
    }


    public virtual void Disable()
    {
        isActive = false;
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            EventManager.PostNotification(EVENT_TYPE.FoundModifier, this);
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;
        }
    }
}
