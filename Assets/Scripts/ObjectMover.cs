using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float moveToPosX;
    [SerializeField] private float moveToPosY;
    [SerializeField] private float unitsPerSecond;
    [SerializeField] private bool destroyInTheEnd;

    private void Awake()
    {

    }


    private void FixedUpdate()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveToPosX, moveToPosY), unitsPerSecond / 50);
        if (destroyInTheEnd && new Vector2(transform.position.x, transform.position.y) == new Vector2(moveToPosX, moveToPosY))
            Destroy(gameObject);

    }
    public void SetDestination(float x, float y, float unitsPerSecond, bool destroyInTheEnd)
    {
        this.destroyInTheEnd = destroyInTheEnd;
        this.unitsPerSecond = unitsPerSecond;
        moveToPosX = x;
        moveToPosY = y;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(moveToPosX, moveToPosY, transform.position.z));
    }
}
