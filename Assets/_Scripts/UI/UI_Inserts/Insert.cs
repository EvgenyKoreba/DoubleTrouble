using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insert : MonoBehaviour
{
    [SerializeField] private float _lifetime = 0f;

    private void Awake()
    {
        if (_lifetime != 0)
        {
            Destroy(gameObject, _lifetime);
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
