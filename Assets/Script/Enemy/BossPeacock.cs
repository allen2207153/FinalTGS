using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossPeacock:MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    public int damagePower;
    private Rigidbody2D rb;
    public float flashTime;
    private Animator anim;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    private Vector3 playerPosition;

    [SerializeField] AudioSource knockbackEfffect;

    //Idel Stage
    [Header("Idel")]
    [SerializeField] float idelMoveSpeed;
    [SerializeField] Vector2 idelMoveDirection;
    //For Walk
    [Header("Walk")]
    [SerializeField] float moveSpeed;
    private float moveDirection = 1;
    private bool facingRight = true;
    [Header("Other")]
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] float circleRadius;
    [SerializeField] LayerMask groundLayer;
    private bool checkingGround;
    private bool checkingWall;

    [Header("For JumpAttacking")]
    [SerializeField] float jumpHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded;

    private bool dropItem = false;
    public GameObject clearTimeline;

    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    public void Update()
    {
        if(health <= maxHealth /2)
        {
            //anim.SetBool("Phase2",true);
        }
        if (health <= 1)
        {

            flashTime = 0;
        }
        if (health <= 0 && dropItem ==false)
        {
            anim.SetBool("Walk", false);
            rb.velocity = Vector3.zero;
            anim.SetBool("Die", true);
            GetComponent<Collider2D>().enabled = false;
            dropItem = true;
            rb.AddForce(new Vector2(800, 700));
            Time.timeScale = 0.4f;
            StartCoroutine(slowTime());
            
        }

    }

     void FixedUpdate()
    {
        checkingGround = Physics2D.OverlapCircle(groundCheckPoint.position, circleRadius, groundLayer);
        checkingWall = Physics2D.OverlapCircle(wallCheckPoint.position, circleRadius, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);
        if(checkingGround || isGrounded)
        {
            anim.SetBool("isGround", true);
        }
        else
        {
            anim.SetBool("isGround", false);
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
                knockbackEfffect.Play();
                playerHealth.DamagePlayer(damagePower);
                other.transform.position = new Vector2(other.transform.position.x - 20 * Time.deltaTime, other.transform.position.y + 20 * Time.deltaTime);
            }
            
        }
        if (other.gameObject.CompareTag("Player") &&anim.GetBool("Walk") == true)
        {
            anim.SetBool("Walk", false);
            anim.SetTrigger("HeadAttack");
        }
    }

    public void Destroy()
    {
        
        Destroy(gameObject);
    }

    public void Patrolling()
    {
        if(!checkingGround || checkingWall)
        {
            if(facingRight)
            {
                Flip();
            }
            else if (!facingRight)
            {
                Flip();
            }
        }
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }

    public void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

      
            anim.SetBool("jump", false);
            rb.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
            if(isGrounded)
        {
            anim.SetBool("jump", false);
        }
    }

    public void FlipTowardsPlayer()
    {
        float playerPosition = player.position.x - transform.position.x;
        if(playerPosition <0 && !facingRight)
        {
            Flip();
        }
        else if (playerPosition >0 && facingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        moveDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheckPoint.position, circleRadius);
        Gizmos.DrawWireSphere(wallCheckPoint.position, circleRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundCheck.position, boxSize);
    }
    public void attackPlayer()
    {
        playerPosition = player.position - transform.position;
        
        anim.SetTrigger("HeadAttack");
    }

    void randomStatePicker()
    {
        int randomState = Random.Range(0, 2);
        if(randomState == 0)
        {
            anim.SetBool("jump", true);
            anim.SetBool("Walk", false);
        }
       if (randomState == 1)
        {
            anim.SetBool("Walk", true);
        }
    }
    public void headAttack()
    {
        float playerDistance = Mathf.Abs(player.position.x - transform.position.x);
        if (playerDistance < 2 && anim.GetBool("isGround") == true)
        {
            anim.SetBool("HeadAttack",true);
            anim.SetBool("Walk", false);
        }
    }
    IEnumerator slowTime()
    {
        yield return new WaitForSeconds(0.7f);
        Time.timeScale = 1;
        clearTimeline.SetActive(true);
    }
    
}
