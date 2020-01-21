using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _BaseForwardPlayerSpeed;
    [SerializeField] private float _BaseStrafePlayerSpeed;
    
    
    private Transform _PlayerTransform;
    private Rigidbody2D _Rigidbody2D;

    private void Awake()
    {
        _PlayerTransform = GetComponent<Transform>();
        _Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 finallyVelocity;
        Vector3 verticalVelocity = transform.up * (Input.GetAxis("Vertical") * _BaseForwardPlayerSpeed * Time.fixedDeltaTime);
        Vector3 horizontalVelocity = transform.right * (Input.GetAxis("Horizontal") * _BaseStrafePlayerSpeed * Time.fixedDeltaTime);
        
        _Rigidbody2D.velocity = verticalVelocity + horizontalVelocity;

        //Debug.Log($"trans.up = {transform.up} / trans.up = {transform.} / Axis = {Input.GetAxis("Vertical")} / Velocity = {_Rigidbody2D.velocity}");
    }
}
