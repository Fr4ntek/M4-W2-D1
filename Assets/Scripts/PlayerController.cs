using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private int _jumpForce = 1;
    private float _walkSpeed = 5f;

    private int _jumpCount = 0;
    private int _maxJumps = 2;
    private Rigidbody _rb;
    private float h;
    private float v;
    Vector3 direction;
    [SerializeField] private GroundChecker _groundChecker;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if(_groundChecker == null) _groundChecker = GetComponentInChildren<GroundChecker>();
    }

  
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        
        direction = new Vector3(h, 0, v).normalized;
        if (h != 0 || v != 0)
        {
            transform.forward = direction; //alternativa Quaternion.LookRotation(transform.forward, direction);
        }

        
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            _jumpCount++;
            _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _walkSpeed * 2;
        }
        else
        {
            _speed = _walkSpeed;
        }

        
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + direction * (_speed * Time.fixedDeltaTime));
    }
    
    private bool CanJump()
    {
        if (_groundChecker.IsGrounded)
        {
            _jumpCount = 0;
        }

        return _jumpCount < _maxJumps;

    }
}
