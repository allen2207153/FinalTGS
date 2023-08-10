using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxLifeUp : MonoBehaviour
{
    private PlayerHealth playerHealth;

    public static event Action OnMaxHealthUp;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") &&
            other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            playerHealth.maxHealth += 1;
            
            OnMaxHealthUp?.Invoke();
            Destroy(gameObject);
        }
    }
}
