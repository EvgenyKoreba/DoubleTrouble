using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header(""), Space(10)]
    [SerializeField] private GameObject FocusObject;
    [SerializeField] private float cameraFollowSpeed;



    void Update()
    {
        Vector3 posOfFocus = new Vector3(FocusObject.transform.position.x, FocusObject.transform.position.y, -10.0f);
        Vector3 posOfCamera = transform.position;
        transform.position = Vector3.Lerp(posOfCamera, posOfFocus , Time.deltaTime*cameraFollowSpeed);
        
    }
}
