using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insert : MonoBehaviour
{
    [SerializeField] private float lifetime = 0f;


    private void Awake()
    {
        if (lifetime != 0)
        {
            Destroy(gameObject, lifetime);
        }
        else
        {
            SetListeners();
        }
    }


    protected virtual void SetListeners()
    {

    }
}
