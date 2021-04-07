using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private float timeBetweenAttacks = 0;
    public float startTimeBetweenAttacks;
    public Transform attackPosition;
    public float attackRange;

    // Temporary to see the attack position
    public GameObject attack; 
    // Update is called once per frame
    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.F))
            {
                // Attack
                Debug.Log("Player just tried to attack");
                attack.SetActive(!attack.activeInHierarchy); // take this out 
                Debug.Log(attack.activeInHierarchy); 

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPosition.position, attackRange);

                for (int i = 0; i < enemiesToDamage.Length; i++ )
                {
                    // Do something with:
                    // enemiesToDamage[i] 
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
        Gizmos.DrawWireSphere(attackPosition.position, attackRange); 
    }
}
