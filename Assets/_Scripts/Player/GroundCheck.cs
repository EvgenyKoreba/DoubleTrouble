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

    private bool _isGround = false;
    #endregion


    public bool IsGrounded
    {
        get { return _isGround; }
    }

    private void Update()
    {
        _isGround = Physics2D.OverlapCircle(transform.position, _groundRadius, _ground);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _groundRadius);
    }
}
