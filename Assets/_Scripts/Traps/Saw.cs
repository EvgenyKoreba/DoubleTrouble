using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovingBehaviour))]
public class Saw : DamagingBehaviour
{
    private MovingBehaviour _movingBehaviour;

    private void Start()
    {
        TurnOn();
    }

    public void TurnOn()
    {
        _movingBehaviour = GetComponent<MovingBehaviour>();
        if (_movingBehaviour != null)
        {
            _movingBehaviour.Move();
        }
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
