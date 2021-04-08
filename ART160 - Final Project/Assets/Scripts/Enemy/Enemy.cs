using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float maxHealthPoints = 10f;
    public float currentHealthPoints = 10f;
    public float movementSpeed = 5f;

    public void TakeDamage(float damage)
    {
        this.currentHealthPoints -= damage; 
    }

    private void Update()
    {
        if (currentHealthPoints <= 0)
        {
            Destroy(this.gameObject); 
        }
    }
}
