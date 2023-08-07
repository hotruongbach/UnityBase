using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavMesh : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed = 90;
    public float gravity = -20f;
    public float jumpSpeed = 15;
    public float slopeForce;
    public float slopeForceRayLenght;

    private CharacterController _characterController;

    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    private bool isJumping;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private float hInput;
    private float vInput;
    private float previousPositionX;

    bool jump;
    bool trigger;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPositionX = Input.mousePosition.x;
            vInput = 1f;
        }
        else if (Input.GetMouseButton(0))
        {
            vInput = 1f;
            hInput = (Input.mousePosition.x - previousPositionX) / 100;
            previousPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            hInput = 0f;
            vInput = 0f;
        }
        else
        {
            hInput = 0f;
            vInput = 0f;
        }
        MoveCharater();
    }

    private void MoveCharater()
    {
        if (vInput > 1)
            vInput = 1;
        if (vInput < -1)
            vInput = -1;
        if (_characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * vInput;
            turnVelocity = transform.right  *speed * hInput;
            if (jump == true)
            {
                moveVelocity.y = jumpSpeed;
                jump = false;
            }
        }

        moveVelocity.y += gravity * Time.deltaTime;

        _characterController.Move((moveVelocity + turnVelocity) * Time.deltaTime);

        if ((hInput != 0 || vInput != 0) && OnSlope())
        {
            _characterController.Move(Vector3.down * _characterController.height / 2 * slopeForce * Time.deltaTime);
        }
    }

    private bool OnSlope()
    {
        if (isJumping)
        {
            return false;
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit,
                _characterController.height / 2 * slopeForceRayLenght))
            if (hit.normal != Vector3.up)
                return true;
        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            if (trigger == true) return;
            jump = true;
            trigger = true;
        }
    }
}

