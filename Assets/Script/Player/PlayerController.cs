using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float climbSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D myfeet;
    public bool isGround;



    [Header("Knockback")]
    [SerializeField] private Transform center;
    [SerializeField] private float knockbackVel = 8f;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    [SerializeField] private Transform groundCheck;
    [SerializeField] public Transform downHitBox;
    [SerializeField] private LayerMask groundLayer;

    private bool isLadder;
    private bool isClimbing;

    private bool isJumping;
    private bool isFalling;
    private float playerGravity;
    public  bool live = true;
    private bool isOneWayPlatform;

    bool freezeInput;
    bool freezePlayer;

    RigidbodyConstraints2D rb2dConstraints;

    private float jumpTime;
    public float jumpStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myfeet = GetComponent<CircleCollider2D>();
        playerGravity = rb.gravityScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveDirX = Input.GetAxis("Horizontal");
        float moveDirY = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(moveDirX, moveDirY);
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth.health <=0)
        {
            freezeInput = true;
            FreezePlayer(true);
        }
        Flip();
        if(freezeInput == false ) 
        {
            Run(dir);
            Jump();
            
        }

        coyote();
        CheckGround();
         Climb();
      // Drill();
         CheckAirState();
         CheckLadder();
        OneWayPlatformCheck();
         SwitchAnimation();

        if (Input.GetKeyDown(KeyCode.K))
        {
            FreezeInput(!freezeInput);
            Debug.Log("Freeze Input: " + freezeInput);
        }

       
    }
     private void FixedUpdate()
     {
         downAttackForce();

     }

     void Run(Vector2 dir)
     {
        if (!freezeInput)
        {
            Vector2 playerVelocity = new Vector2(dir.x * speed, rb.velocity.y);
            rb.velocity = playerVelocity;
            bool playerHasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
            anim.SetBool("Run", playerHasSpeed);
            if(anim.GetBool("isAttack") == true &&isGround)
            {
                rb.velocity = new Vector2(0,rb.velocity.y);
            }
        }
     }
     void Flip()
     {
         bool playerHasSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
         if(playerHasSpeed) 
         {
             if(rb.velocity.x > 0.1f)
             {
                 transform.localRotation = Quaternion.Euler(0, 0, 0);
             }
             if (rb.velocity.x < -0.1f)
             {
                 transform.localRotation = Quaternion.Euler(0, 180, 0);
             }
         }
     }

     void Jump()
     {
        if (freezeInput==false)
        {
            if (Input.GetButtonDown("Jump")&&isGround == true )
            {
                 anim.SetBool("Jump", true);
                rb.velocity = Vector2.up * jumpForce;
                isJumping = true;
                jumpTime = jumpStartTime;
            }
            if(Input.GetButtonDown("Jump") && isJumping == true)
            {
                if(jumpTime > 0) 
                {
                    rb.velocity  = rb.velocity = Vector2.up * jumpForce;
                    jumpTime -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }
            if(Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }
         }
     }
    void coyote()
    {
        if (isGround ||isOneWayPlatform)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;

        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

            coyoteTimeCounter = 0f;
        }
    }
        void SwitchAnimation()
     {
         anim.SetBool("Idle", false);
         if(anim.GetBool("Jump"))
         {
             if(rb.velocity.y <0.0f)
             {
                 anim.SetBool("Jump", false);
                 anim.SetBool("Fall", true);
             }
         }
         else if (isGround || isOneWayPlatform)
         {
             anim.SetBool("Fall", false);
             anim.SetBool("Idle", true);
             anim.SetBool("downAttack", false);
         }
     }

     void Climb()
     {
         if(isLadder)
         {
             float moveY = Input.GetAxis("Vertical");
             if(moveY >0.5f || moveY < -0.5f¡@)
             {
                 anim.SetBool("Climbing", true);
                anim.SetBool("ClimbIdle", false);
                rb.gravityScale = 0.0f;
                 rb.velocity = new Vector2(rb.velocity.x, moveY *climbSpeed);
             }
             else
             {
                 if(isJumping || isFalling )
                 {
                     anim.SetBool("Climbing",false);
                    anim.SetBool("ClimbIdle", false);
                }
                 else
                 {
                     anim.SetBool("Climbing", false);
                    anim.SetBool("ClimbIdle", true);
                     rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                 }
             }
         }
         else
         {
             anim.SetBool("Climbing", false);
            anim.SetBool("ClimbIdle", false);
            rb.gravityScale = playerGravity;
         }


     }

        void CheckAirState()
        {
            isJumping = anim.GetBool("Jump");
            isFalling = anim.GetBool("Fall");
            isClimbing = anim.GetBool("Climbing");
        }

        void CheckGround()
    {
        isGround = myfeet.IsTouchingLayers(LayerMask.GetMask("Ground")) ||
                            myfeet.IsTouchingLayers(LayerMask.GetMask("MovingPlatform")) ||
                            myfeet.IsTouchingLayers(LayerMask.GetMask("Rock")) ||
                           myfeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        isOneWayPlatform = myfeet.IsTouchingLayers(LayerMask.GetMask("OneWayPlatform"));
        
    }

    void CheckLadder()
    {
        isLadder = myfeet.IsTouchingLayers(LayerMask.GetMask("Ladder"));
    }

    void downAttackForce()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("downAttack"))
        {
            Collider2D c = Physics2D.OverlapBox(downHitBox.position, new Vector2(0.23f, 0.1f), 0f, LayerMask.GetMask("Enemy") | LayerMask.GetMask("Rock") | LayerMask.GetMask("latern")) ;
            if (c != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, 800));
                GetComponent<Better>().fallMultiplier = 2f;
                GetComponent<Better>().lowJumpMultiplier = 2f;
  
            }
        }
        else
        {

        }
    }
    
    
    void OneWayPlatformCheck()
    {
            if(isGround&& gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
            float moveY = Input.GetAxis("Vertical");
                if(isOneWayPlatform && moveY <-0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", 0.3f);
        }
    }

    void RestorePlayerLayer()
    {
        if(!isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

   /* void Drill()
    {
        if (Input.GetButtonUp("Drill") && anim.GetBool("Run") == false)
        {
            anim.SetBool("Drilling", true);
            StartCoroutine(StartAttack());
        }
        IEnumerator StartAttack()
        {

            yield return new WaitForSeconds(delayTime);
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0,700));
            anim.SetBool("Drilling", false);

        }
    }*/

    public void FreezeInput(bool freeze)
    {
        // freeze/unfreeze user input
        freezeInput = freeze;

    }
   
    public void FreezePlayer(bool freeze)
    {
        // freeze/unfreeze the player on screen
        // zero animation speed and freeze XYZ rigidbody constraints
        if (freeze)
        {
            freezePlayer = true;
            rb2dConstraints = rb.constraints;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            freezePlayer = false;
            anim.speed = 1;
            rb.constraints = rb2dConstraints;
        }
    }
  
   
}



