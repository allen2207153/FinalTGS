using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float climbSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D myfeet;
    public bool isGround;

    [SerializeField] Vector2 boxSize;

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

     [SerializeField] float buttonPressedTime;
    [SerializeField] float buttonPressWindow;
    private bool jumpCancelled;


    [SerializeField] float jumpHeight = 5;
    [SerializeField] float gravityScale = 5;
    [SerializeField] float fallGravityScale = 15;

    [SerializeField] private AudioSource jumpEffect;
    [SerializeField] private AudioSource downAttackEffect;


    public float drillForce = 800;

    [Header ("Music")]
    public AudioClip newTrack;
    private AudioManager theAM;

    [Header ("Scene")]
    public GameObject endScene;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myfeet = GetComponent<CircleCollider2D>();
        playerGravity = rb.gravityScale;
        theAM = FindObjectOfType<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        float moveDirX = Input.GetAxis("Horizontal");
        float moveDirY = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(moveDirX, moveDirY);
        PlayerHealth playerHealth = GetComponent<PlayerHealth>();
        if (playerHealth.health <=0 &&isGround)
        {
            freezeInput = true;
            rb.velocity = Vector3.zero;
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
            if (Input.GetButtonDown("Jump")&&isGround && rb.velocity.y < 0.01f)
            {
                 anim.SetBool("Jump", true);
                jumpEffect.Play();
                rb.gravityScale = gravityScale;
                float jumpForce = Mathf.Sqrt(jumpHeight * (Physics2D.gravity.y * rb.gravityScale) * -2) * rb.mass;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
                buttonPressedTime = 0;
            }
            if(isJumping)
            {
                buttonPressedTime +=Time.deltaTime;

                if(buttonPressedTime <buttonPressWindow && Input.GetButtonDown("Jump"))
                {
                    jumpCancelled = true;
                }
                if(rb.velocity.y <0 || buttonPressedTime>buttonPressWindow)
                {
                    rb.gravityScale = fallGravityScale;
                    isJumping=false;
                }
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
            Collider2D c = Physics2D.OverlapBox(downHitBox.position, boxSize, 0f, LayerMask.GetMask("Enemy") | LayerMask.GetMask("Rock") | LayerMask.GetMask("latern")) ;
            if (c != null)
            {
                downAttackEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, drillForce));
                GetComponent<Better>().fallMultiplier = 2f;
                GetComponent<Better>().lowJumpMultiplier = 2f;
  
            }
        }
      
    }
    
    
    void OneWayPlatformCheck()
    {
        if (isGround && gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        float moveY = Input.GetAxis("Vertical");
        if (isOneWayPlatform && moveY < -0.1f)
        {
            gameObject.layer = LayerMask.NameToLayer("OneWayPlatform");
            Invoke("RestorePlayerLayer", 0.4f);
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
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.color = Color.green;
        Gizmos.DrawCube(downHitBox.position, boxSize);
    }
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Crystal"))
        {
            anim.SetTrigger("getCrystal");
            FreezePlayer(true);
            FreezeInput(true);
            theAM.ChangeBGM(newTrack);
            StartCoroutine(stop());
        }
    }
    IEnumerator stop()
    {
        yield return new WaitForSeconds(3.2f);
        theAM.stopMusic();
        endScene.SetActive(true);
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("ending");
    }
}



