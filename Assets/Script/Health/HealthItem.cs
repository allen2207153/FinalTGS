using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public int health;
    public static event Action OnPlayerHeal;

  public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        health = playerHealth.health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")
           )
        {
            
            OnPlayerHeal?.Invoke();
            Destroy(gameObject);
        }
    }
}
