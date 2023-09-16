using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;
    public float attackRate = 0.25f;
    public float attackTime = 0.5f;
    private Animator anim;
    private PolygonCollider2D collider2D;
    GameObject player;
    [SerializeField] private AudioSource attackEffect;
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
        if (attackRate < attackTime)
        {
            attackRate += Time.deltaTime;
        }
    }
    void Attack()
    {
       
        if (Input.GetButtonDown("Attack") && anim.GetBool("isDead") !=true && anim.GetBool("hurtZone") != true)
        {
            
            if (attackRate >= attackTime)
            {
                StartCoroutine(StartAttack());
                anim.SetTrigger("Attack");
                attackEffect.Play();
                anim.SetBool("isAttack", true);
                attackRate = 0;
               
            }

            
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
        if (other.gameObject.CompareTag("Bosspeacock"))
        {
            other.GetComponent<BossPeacock>().TakeDamage(damage);
        }


    }



    

}
