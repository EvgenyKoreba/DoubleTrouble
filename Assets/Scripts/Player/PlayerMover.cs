using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMover : MonoBehaviour
{
    #region Fields
    [Header("Set in Inspector: Move Options")]
    [SerializeField] private float speed;


    [Header("Set Dynamically"), Space(10)]
    [SerializeField] private bool _facingRight = true;


    private Rigidbody2D _rigidBody;
    private Animator _animator;
    #endregion


    public bool facingRight { 
        get { return _facingRight; } 
        private set { _facingRight = value; }
    }


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

    }

    private void FixedUpdate()
    {
        // Move
        float moveInput = Input.GetAxis("Horizontal");
        _rigidBody.velocity = new Vector2(moveInput * speed, _rigidBody.velocity.y);


        if (moveInput > 0)
        {
            facingRight = true;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //_animator.Play("RunAnimation");
        }
        else if (moveInput < 0)
        {
            facingRight = false;
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            //_animator.Play("RunAnimation");
        }
        else
        {
            //_animator.Play("IdleAnimation");
        }

        _animator.SetFloat("HorizontalSpeed", Mathf.Abs(_rigidBody.velocity.x));
        _animator.SetFloat("VerticalSpeed", _rigidBody.velocity.y);

        Debug.Log(moveInput > 0);
    }
}
