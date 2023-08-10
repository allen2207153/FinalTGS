using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss:MonoBehaviour
{
    public int health;
    public int damagePower;
    private Rigidbody2D rb;
    public float flashTime;
    private Animator anim;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 1)
        {

            flashTime = 0;
        }
        if (health <= 0)
        {
            anim.SetTrigger("Die");

        }

    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        FlashColor(flashTime);
    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    void ResetColor()
    {
        sr.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damagePower);
                other.transform.position = new Vector2(other.transform.position.x - 20 * Time.deltaTime, other.transform.position.y + 20 * Time.deltaTime);
            }
        }
    }

    void Destroy()
    {
        
        Destroy(gameObject);
    }
}
