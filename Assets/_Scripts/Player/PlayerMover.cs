using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: Move Options")]
    [Range(0,20)]
    [SerializeField] private float _speed;

    [Header("Set Dynamically"), Space(10)]
    [SerializeField] private bool _facingRight = true;

    private Rigidbody2D _rigidBody;
    private Animator _animator;
    #endregion

    #region Properties
    public bool FacingRight
    {
        get => _facingRight;
        private set => _facingRight = value;
    }
    #endregion

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Move
        float moveInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(moveInput * _speed, _rigidBody.velocity.y);


        if (moveInput > 0)
        {
            FacingRight = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //_animator.Play("RunAnimation");
        }
        else if (moveInput < 0)
        {
            FacingRight = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            //_animator.Play("RunAnimation");
        }
        else
        {
            //_animator.Play("IdleAnimation");
        }

        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat("VerticalSpeed", _rigidBody.velocity.y);
    }
}
