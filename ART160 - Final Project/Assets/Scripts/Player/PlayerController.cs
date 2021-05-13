using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Inventory
    public GameObject playerInventory;
    #endregion

    #region Movement Variables
    public Rigidbody2D playerRB;
    public float playerHealth;
    public bool isDead;

    public float movementSpeed = 5f; // 5 by default

    public float dashSpeed = 15f;
    public float timeBetweenLastDash = 0f;
    public float timeBetweenDashes = 0f;


    public float jumpForce = 7f;
    public bool grounded;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float horizontalThrust = 55f;
    public float verticalThrust = 10f;

    public GameObject startButton;
    public GameObject quitButton; 

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
        // Checking for inputs

        if (!isDead)
        {
            Vector2 movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            if (movementVector == Vector2.zero)
            {
                playerAnimator.SetBool("isWalking", false);
            }
            TurnAround(movementVector);
            Move(movementVector);

            if (Input.GetKeyDown(KeyCode.T) && timeBetweenLastDash <= 0)
            {
                Debug.Log("Attemping to dash");
                Dash(movementVector);
                timeBetweenLastDash = timeBetweenDashes;
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                InteractWithInventory();
            }

            if (Input.GetButtonDown("Jump") && grounded)
            {
                Jump();

                grounded = false;
            }

            SmoothenJump();
            timeBetweenLastDash -= Time.deltaTime;
        }

        else if (isDead)
        {
            // Display restart level button
            playerAnimator.SetBool("isDead", true); 
        }

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
        if (directionVector == Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", false); 
        }

        else
        {
            playerAnimator.SetBool("isWalking", true);
        }
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

    private void Dash(Vector2 directionVector)
    {
        playerAnimator.SetTrigger("rollTrigger");
        playerRB.velocity = new Vector2(playerRB.velocity.x * dashSpeed, playerRB.velocity.y);
        // playerRB.velocity = new Vector2(directionVector.x * dashSpeed, playerRB.velocity.y);
    }

    private void InteractWithInventory()
    {
        if (playerInventory.activeInHierarchy)
        {
            playerInventory.SetActive(false); 
        }

        else
        {
            playerInventory.SetActive(true);
        }
    }

    public void TakeDamage(float attackDamage, float enemyX)
    {
        playerHealth -= attackDamage;
        // To the right
        playerRB.velocity = Vector3.zero;
        playerRB.angularVelocity = 0; 
        if (enemyX > transform.position.x)
        {
            playerRB.AddForce(new Vector3(-1 * horizontalThrust, 1 * verticalThrust, 0), ForceMode2D.Impulse);

        }

        else if (enemyX < transform.position.x)
        {
            playerRB.AddForce(new Vector3(1 * horizontalThrust, 1 * verticalThrust, 0), ForceMode2D.Impulse);
        }

        if (playerHealth <= 0)
        {
            isDead = true;

            startButton.SetActive(true);
            quitButton.SetActive(true); 
        }
    }

    #endregion 
}
