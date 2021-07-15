using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementTypes;

[RequireComponent(typeof(MovementBehaviour))]
public class Saw : DamagingBehaviour
{
    private MovementBehaviour _movingBehaviour;

    private void Start()
    {
        TurnOn();
    }

    public void TurnOn()
    {
        _movingBehaviour = GetComponent<MovementBehaviour>();
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
