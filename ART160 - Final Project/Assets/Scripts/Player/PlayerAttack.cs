using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackDamage = 5f; 
    private float timeBetweenAttacks = 0;
    public float startTimeBetweenAttacks;
    public Transform attackPosition;
    public float attackRadius;

    public Animator playerAnimator;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Pressed attack button");
                playerAnimator.SetTrigger("attackTrigger"); 
                Collider2D[] thingsToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRadius);

                for (int i = 0; i < thingsToDamage.Length; i++ )
                {
                    if (thingsToDamage[i].gameObject.tag == "Enemy")
                    {
                        Debug.Log("Hit something!"); 
                        thingsToDamage[i].GetComponent<Enemy>().TakeDamage(attackDamage, transform.position.x); 
                    }
                }
            }
            timeBetweenAttacks = startTimeBetweenAttacks;
        }

        else
        {
            timeBetweenAttacks -= Time.deltaTime; 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius); 
    }
}
