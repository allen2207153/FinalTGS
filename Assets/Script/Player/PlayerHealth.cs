using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health, maxHealth;

    public int Blinks;
    public float time;
    public float dieTime;
    public float delayTime;
    [SerializeField] private float invincCooldown = 3f;

    private SpriteRenderer myRender;
    private Animator anim;
    public static event Action OnPlayerDamaged;


    void Start()
    {
        myRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void DamagePlayer(int damage)
    {

        health -= damage;
        OnPlayerDamaged?.Invoke();

        if (health > 0)
        {
            hurtState();
            BlinkPlayer(Blinks, time);
        }
        if (health <= 0)
        {
            Death();
        }

    }


    void BlinkPlayer(int numBlink, float seconds)
    {
        StartCoroutine(DoBlink(numBlink, seconds));
    }
    IEnumerator DoBlink(int numBlink, float seconds)
    {
        for (int i = 0; i < numBlink; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
    void hurtState()
    {
        StartCoroutine(hurt());

        IEnumerator hurt()
        {

            anim.SetTrigger("hurt");
            Physics2D.IgnoreLayerCollision(9, 10, true);
            anim.SetBool("hurtZone", true);
            yield return new WaitForSeconds(invincCooldown);

            anim.SetBool("hurtZone", false);
            Physics2D.IgnoreLayerCollision(9, 10, false);
        }
    }
    void Death()
    {
        Physics2D.IgnoreLayerCollision(9, 10, true);
        anim.SetTrigger("Death");
        anim.SetBool("isDead", true);
            StartCoroutine(restart());
        }
    IEnumerator restart()
    {

        yield return new WaitForSecondsRealtime(delayTime);
        SceneManager.LoadScene("Stage1");
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
    
 
}
