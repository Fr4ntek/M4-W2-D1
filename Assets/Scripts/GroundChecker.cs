using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private float _maxDistance = 0.1f;
    [SerializeField] private LayerMask _whatIsGround;
    
    
    public bool IsGrounded {  get; private set; }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _maxDistance);
    }

    void Start()
    {
       
    }

    
    void Update()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, _maxDistance, _whatIsGround);
    }
}
