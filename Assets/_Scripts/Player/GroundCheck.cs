using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    #region Fields
    [Header("       Set in Inspector:")]
    [SerializeField] private float _groundRadius;
    [SerializeField] private LayerMask _ground;
    [SerializeField] private float _groundRememberTime = 0.2f;

    private bool _isGroundTouched = false;
    private float _currentGroundRemember = 0;
    #endregion

    private void Awake()
    {
        _currentGroundRemember = _groundRememberTime;
    }

    public bool IsGrounded
    {
        get { return _currentGroundRemember >= 0; }
    }

    private void FixedUpdate()
    {
        _isGroundTouched = Physics2D.OverlapCircle(transform.position, _groundRadius, _ground);
        _currentGroundRemember -= Time.fixedDeltaTime;

        if (_isGroundTouched)
        {
            _currentGroundRemember = _groundRememberTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _groundRadius);
    }
}
