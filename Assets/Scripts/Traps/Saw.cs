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
        StartCoroutine(SawAnimation());
    }

    private IEnumerator SawAnimation()
    {

        float rotZ = 0;
        while (true)
        {
            rotZ += 5;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotZ));
            yield return null;
        }
    }
}
