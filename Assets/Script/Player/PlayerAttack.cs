using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    private Animator anim;
    private PolygonCollider2D collider2D;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
        player = GameObject.Find("Player");
    }
    
    // Update is called once per frame
    void Update()
    {
            Attack();
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack") && anim.GetBool("isDead") !=true)
        {
            StartCoroutine(StartAttack());
            collider2D.enabled = true;
            anim.SetTrigger("Attack");
            anim.SetBool("isAttack", true);
           
        }
    }




    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;
        StartCoroutine(disableHitBox());
       
    }

    IEnumerator disableHitBox()
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
        anim.SetBool("isAttack", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().TakeDamage(damage);
        }


    }



    

}
