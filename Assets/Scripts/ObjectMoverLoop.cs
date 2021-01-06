using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoverLoop : MonoBehaviour
{
    [SerializeField] private float moveToPosX;
    [SerializeField] private float moveToPosY;
    [SerializeField] private float unitsPerSecond;

    #region Fields
    private float startPosX;
    private float startPosY;
    private bool changeDirection;
    #endregion
    private void Awake()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        changeDirection = false;
    }


    private void FixedUpdate()
    {
        if (new Vector2(transform.position.x, transform.position.y) == new Vector2(startPosX, startPosY))
            changeDirection = false;
        else if (new Vector2(transform.position.x, transform.position.y) == new Vector2(moveToPosX, moveToPosY))
            changeDirection = true;
        if (!changeDirection)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveToPosX, moveToPosY), unitsPerSecond / 50);
        else if (changeDirection)
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(startPosX, startPosY), unitsPerSecond / 50);
    }
    public void SetDestinationLoop(float x, float y, float unitsPerSecond)
    {
        this.unitsPerSecond = unitsPerSecond;
        moveToPosX = x;
        moveToPosY = y;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(moveToPosX, moveToPosY, transform.position.z));
    }
}


