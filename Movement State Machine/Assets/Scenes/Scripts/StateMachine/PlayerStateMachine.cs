using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] Transform orientation;
    bool isMoving;
    bool isSprinting;
    public KeyCode sprintKey;

    [Header("GroundCheck")]
    [SerializeField] float playerHeight;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    public KeyCode jumpKey;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;
    Rigidbody rb;

    //getters & setters
    public Rigidbody Rigidbody { get { return rb; } }
    public float JumpForce { get { return jumpForce; } }
    public bool IsGrounded { get { return isGrounded; } set { isGrounded = value; } }
    public Transform Transform { get { return transform; } }
    public LayerMask WhatIsGround { get { return whatIsGround; } }
    public float PlayerHeight { get { return playerHeight; } }
    public bool IsMoving { get { return isMoving; } }
    public bool IsSprinting { get { return isSprinting; } }
    public float WalkSpeed { get { return walkSpeed; } }
    public float SprintSpeed { get { return sprintSpeed; } }
    public float MovementSpeed { get { return movementSpeed; } set { movementSpeed = value; } }

    public PlayerBaseState currentState;
    public PlayerStateFactory states;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        states = new PlayerStateFactory(this);
        currentState = states.Grounded();
        currentState.EnterState();
    }

    // Update is called once per frame
    void Update()
    {
        //IsGrounded
        isGrounded = HandleGrounded();

        HandleInput();
        SpeedControl();

        currentState.UpdateStates();
    }

    private void FixedUpdate()
    {
        Move();
        currentState.FixedUpdateStates();
    }

    void HandleInput()
    {
        //get WASD inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //update isMoving & isSprinting inputs
        isMoving = (horizontalInput != 0 || verticalInput != 0);
        isSprinting = Input.GetKey(sprintKey);
    }

    void Move()
    {
        //apply force to move direction
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(movementDirection.normalized * movementSpeed, ForceMode.Force);
    }

    void SpeedControl()
    {
        //max cap the velocity to the movement speed
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > movementSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    bool HandleGrounded()
    {
        //check if its grounded by shooting a raycast below the player and check if there is a ground
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }
}
