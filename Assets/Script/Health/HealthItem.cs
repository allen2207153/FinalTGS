using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public static event Action OnPlayerHeal;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
           )
        {
            if (playerHealth.health < playerHealth.maxHealth)
            {
                playerHealth.health += 1;
                AudioManager.pickUpHealth();
            }
            else if (playerHealth.health == playerHealth.maxHealth)
            {
                playerHealth.health += 0;
                AudioManager.pickUpHealth();
            }
            
            OnPlayerHeal?.Invoke();
            Destroy(gameObject);
        }
    }

   
       
    
}
