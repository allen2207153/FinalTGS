using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAttack : MonoBehaviour
{
    public int damage;
    public float jumpForce = 20f;

    private Animator anim;
    private PolygonCollider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        downAttack();
    }
    void downAttack()
    {
        if (Input.GetKey("down") || Input.GetAxis("Vertical") == 1)
        {
            if (anim.GetBool("Jump") == true || anim.GetBool("Fall") == true)
            {
                collider2D.enabled = true;
                anim.SetTrigger("downAttack");
      
            }
          

            }
            if (anim.GetBool("Idle") == true)
            {
                anim.SetBool("downAttack", false);
                collider2D.enabled = false;
            }
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Enemy>().TakeDamage(damage);

            }

        }
    
}
