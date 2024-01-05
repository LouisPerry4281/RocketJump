using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float playerHeight = 2f;


    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float airMultiplier = 0.4f;
    float movementMultiplier = 10f;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Jumping")]
    public float jumpForce = 5f;
    public float coyoteTime = 0.2f;
    [HideInInspector] public float coyoteTimeCounter;
    bool isJumping = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.2f;
    public bool isGrounded { get; private set; }

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    [HideInInspector] public Rigidbody rb;

    RaycastHit slopeHit;


    private bool OnSlope()
    {
        //This checks if the player is currently on a slope or flat ground by shooting a raycast down and comparing the returned normal to Vector3.up, returning a bool
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {

        //Checks for if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        MyInput();
        ControlDrag();
        ControlSpeed();

        if (isGrounded)
        {
            //While the player is grounded, their coyote time is constantly set to the max
            coyoteTimeCounter = coyoteTime;
        }

        else
        {
            //When the player is no longer grounded, decrease the coyote time over time
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(jumpKey) && coyoteTimeCounter > 0 && !isJumping)
        {
            //If the player jumps while still within coyote time and is not already jumping, remove coyote time, begin the jump function and put the jump on a minor cooldown
            coyoteTimeCounter = -5f;
            Jump();
            StartCoroutine(JumpCooldown());
        }

        //Finds the tangent movement direction relative to the normal of the plane the player is on
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    void MyInput()
    {
        //Grabs input and determines direction
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
    }

    void Jump()
    {
        //Zeros y velocity and adds the jumpforce, disables coyote time
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        coyoteTimeCounter = 0f;
    }

    void ControlSpeed()
    {
        //If the player is holding the sprint key and grounded, use the sprint speed multiplier, else use the walk speed multiplier
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    void ControlDrag()
    {
        //Switches the drag amount depending on whether or not the player is *Airborne* (See what I did there???)
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        //While grounded and not on a slope, move normally
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        //While grounded and on a slope, use the tangent movement direction
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        //While the player is not grounded, apply a multiplier to their speed
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private IEnumerator JumpCooldown()
    {
        //Handles the jump cooldown (Prevents weird bugs with spamming the button too fast)
        isJumping = true;
        yield return new WaitForSeconds(0.2f);
        isJumping = false;
    }
}