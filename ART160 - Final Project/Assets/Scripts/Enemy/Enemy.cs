using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealthPoints = 10f;
    public float currentHealthPoints = 10f;
    public float movementSpeed = 1f;
    public LineOfSight enemyVision;
    public AttackRange enemyAttackRange; 
    private Animator enemyAnimator;
    public Rigidbody2D enemyRB; 

    public float attackDamage = 5f;
    public float timeBetweenAttacks = 5f;
    public float startTimeBetweenAttacks;
    public float attackRadius;
    public Transform attackPosition;
    public float distanceToPlayer;
    public GameObject player;

    public float originalScaleX;
    public float originalScaleY;
    public float originalScaleZ;

    public bool isDead;
    public float thrust = 1f;

    private void Start()
    {
        enemyRB = this.GetComponent<Rigidbody2D>(); 
        enemyAnimator = this.GetComponent<Animator>();
        originalScaleX = transform.localScale.x;
        originalScaleY = transform.localScale.y;
        originalScaleZ = transform.localScale.z; 
    }

    private void Update()
    {
        if (!isDead)
        {
            if (currentHealthPoints <= 0)
            {
                isDead = true; 
                enemyAnimator.SetBool("isDead", true);
            }

            if (enemyVision.player != null)
            {
                Walk();
            }

            if (enemyVision.player == null)
            {
                enemyAnimator.SetBool("isIdle", true);
                enemyAnimator.SetBool("isWalking", false);
            }

            if (enemyAttackRange.canAttack)
            {
                if (timeBetweenAttacks <= 0)
                {
                    Debug.Log("I'm attacking the player!");
                    enemyAnimator.SetTrigger("attackTrigger");

                    Collider2D[] thingsToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);

                    for (int i = 0; i < thingsToDamage.Length; i++)
                    {
                        if (thingsToDamage[i].gameObject.tag == "Player")
                        {
                            Debug.Log("Attacking right now"); 
                            thingsToDamage[i].GetComponent<PlayerController>().TakeDamage(attackDamage, transform.position.x);
                        }
                    }
                    timeBetweenAttacks = startTimeBetweenAttacks; 
                }
            }

            if (!enemyAttackRange.canAttack)
            {
                enemyAnimator.SetBool("isIdle", true);
                enemyAnimator.SetBool("isWalking", false);
            }

            timeBetweenAttacks -= Time.deltaTime;
        }
        
    }

    public void TakeDamage(float damage, float enemyX)
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (enemyX > transform.position.x)
        {
            Debug.Log("Moving left");
            targetPosition.x -= thrust;
            transform.position = targetPosition;

        }

        else if (enemyX < transform.position.x)
        {
            Debug.Log("Moving right"); 
            targetPosition.x += thrust;
            transform.position = targetPosition;
        }

        this.currentHealthPoints -= damage;

    }

    public float absoluteValue(float num)
    {
        if (num < 0)
        {
            return num * -1;
        }
        return num; 
    }

    private void Walk()
    {

        Vector2 targetPosition = new Vector2(enemyVision.player.transform.position.x + distanceToPlayer, transform.position.y);
        
        if (player.transform.position.x - transform.position.x < 0)
        {
             transform.localScale = new Vector3(originalScaleX, originalScaleY, originalScaleZ);
        }

        else if (player.transform.position.x - transform.position.x >= 0)
        {
            transform.localScale = transform.localScale = new Vector3(-1 * originalScaleX, originalScaleY, originalScaleZ);
        }

        if (targetPosition == new Vector2(transform.position.x, transform.position.y))
        {
            enemyAnimator.SetBool("isIdle", true);
            enemyAnimator.SetBool("isWalking", false);
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            enemyAnimator.SetBool("isIdle", false);
            enemyAnimator.SetBool("isWalking", true);
        }
    }
}

