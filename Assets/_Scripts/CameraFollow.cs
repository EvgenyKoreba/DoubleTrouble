using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region
    [Header("Set in Inspector")]
    [SerializeField] private GameObject _poi;
    [Range(0,1)]
    [SerializeField] private float _easing;
    [Range(0,10)]
    [SerializeField] private float _cameraDisplacement;

    private PlayerMover _playerMover;
    #endregion

    private void Awake()
    {
        _playerMover = _poi.GetComponent<PlayerMover>();
    }

    private void FixedUpdate()
    {
        if (_playerMover == null)
        {
            FollowPOI();
        }
        else
        {
            FollowPlayer();
        }
    }

    public void FollowPOI()
    {
        Vector3 destination = _poi.transform.position;
        destination.z = transform.position.z;
        destination = Vector3.Lerp(transform.position, destination, _easing);
        transform.position = destination;
    }

    private void FollowPlayer()
    {
        Vector3 destination = _playerMover.FacingRight
            ? new Vector3(_playerMover.transform.position.x + _cameraDisplacement, _playerMover.transform.position.y, transform.position.z)
            : new Vector3(_playerMover.transform.position.x - _cameraDisplacement, _playerMover.transform.position.y, transform.position.z);
        destination = Vector3.Lerp(transform.position, destination, _easing);
        transform.position = destination;
    }
}
