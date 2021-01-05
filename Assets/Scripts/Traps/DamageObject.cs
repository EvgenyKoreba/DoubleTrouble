using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    [SerializeField] private float moveToPosX;
    [SerializeField] private float moveToPosY;
    [SerializeField] private float unitsPerSecond;
    [SerializeField] private bool isLooping;

    #region FieldsForLoopingObjects
    private float startPosX;
    private float startPosY;
    private bool changeDirection;
    #endregion
    private void Awake()
    {
        if (isLooping)
        {
            startPosX = transform.position.x;
            startPosY = transform.position.y;
            changeDirection = false;
        }
    }


    private void FixedUpdate()
    {
        if (isLooping)
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
        else if (!isLooping)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveToPosX, moveToPosY), unitsPerSecond / 50);
            if (new Vector2(transform.position.x, transform.position.y) == new Vector2(moveToPosX, moveToPosY))
            {
                Destroy(gameObject);
            }
        }
    }
    public void SetDestination(float x , float y , bool isZalooping)
    {
        moveToPosX = x;
        moveToPosY = y;
        isLooping = isZalooping;
    }
}
