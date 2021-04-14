using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region Movement Variables
    public Rigidbody2D playerRB;

    public float movementSpeed = 5f; // 5 by default

    public float dashSpeed = 7f;
    public float timeBetweenLastDash = 0f;
    public float timeBetweenDashes = 3f;


    public float jumpForce = 7f;
    public bool grounded;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    #endregion

    #region Animation Variables

    // Initially, the player is facing left! 
    public bool facingLeft = true;
    public bool facingRight = false;

    public Animator playerAnimator; 
    #endregion 

    #region General Behavior Loop

    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>(); 

    }

    private void Update()
    { 
        Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        TurnAround(movementVector); 
        Move(movementVector);

        if (Input.GetKeyDown(KeyCode.T) && timeBetweenLastDash <= 0)
        {
            Dash();
            timeBetweenLastDash = timeBetweenDashes; 
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();

            grounded = false; 
        }

        SmoothenJump();
        timeBetweenLastDash -= Time.deltaTime; 
    }


    // Checking if player returns to the ground and able to jump again
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Ground") && grounded == false)
        {
            grounded = true; 
        }
    }

    #endregion 

    #region Player Movement Functions

    private void Move(Vector2 directionVector)
    {
        playerRB.velocity = new Vector2(directionVector.x * movementSpeed, playerRB.velocity.y);
    }

    private void Jump()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0);
        playerRB.velocity += Vector2.up * jumpForce;
    }

    private void SmoothenJump()
    {
        if (playerRB.velocity.y < 0)
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (playerRB.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;

        }
    }

    private void TurnAround(Vector2 directionVector)
    {
        if (directionVector == new Vector2(1, 0))
        {
            facingRight = true;
            facingLeft = false; 
        }

        else if (directionVector == new Vector2(-1, 0))
        {
            facingLeft = true;
            facingRight = false; 
        }

        if (facingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (facingLeft)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Dash()
    {
        playerRB.velocity = new Vector2(playerRB.velocity.x * dashSpeed, playerRB.velocity.y);
    }

    #endregion 
}
