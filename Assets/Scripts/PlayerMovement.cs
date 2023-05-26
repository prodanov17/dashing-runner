using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [Space]
    public CameraShake cameraShake;
    public Rigidbody2D rb;
    public KeyHolder kh;
    public Trail trail;
    public GameObject finish;
    public ParticleSystem dust, landDust, jumpDust, dashEffect;
    public Animator transition;

    private MovingPlatform mp;

    [Space]
    [Header("Movement")]
    [Space]
    public float moveSpeed = 5f;
    public bool Grounded = true;
    private bool canMove = true;

    [Space]

    [Header("Jumping")]
    [Space]
    public float jumpHeight = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    private float launch;
    private bool jump = false;
    private bool jumpRequest;
    private bool onSpringboard = false;
    
    [Space]

    [Header("Dashing")]
    public float dashSpeed = 4f;
    public float dashCooldown = .5f;
    public float startDashTimer;
    private int dashCount = 0;
    private float nextTimeToDash = 0f;
    private float dashTimer;
    private bool isDashing = false;

    [Space]

    [Header("Wall Jumping")]
    public float slideSpeed = 1f;
    private float wallJumpTime = 0.2f;
    private float jumpTime;
    private bool onWall = false;

    //Platforms
    private float platformSpeed;
    private bool onPlatform = false;


    private void Update()
    {
        if (onWall && !Grounded)
        {
            WallSlide();
            jumpTime = Time.time + wallJumpTime;
        }
        else if (onWall && Grounded)
        {

            onWall = false;
            GetComponent<Animator>().SetBool("onWall", false);
        }

        //Jumping
        if (Input.GetButtonDown("Jump") && Grounded == true)
        {
            jumpRequest = true;
        }


        if (Input.GetKeyDown(KeyCode.F) && dashCount < 1)
        {
           if(Time.time > nextTimeToDash)
            {
                AudioManager.instance.Play("Dash");
                //dashEffect.Play();
                isDashing = true;
                dashCount += 1;
                dashTimer = startDashTimer;
            }
        }

        //Check landing
        if (jump)
        {
            if (Grounded)
            {
                jump = false;
                Land();
            }
        }
    }
    void FixedUpdate()
    {
        if (canMove) Move();

        if (jumpRequest)
        {
            GetComponent<Animator>().SetBool("isJumping", true);
            Jump();
            jumpRequest = false;
        }

        //Better Jump
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else rb.gravityScale = 1f;


        if (isDashing)
        {

            GetComponent<Animator>().SetBool("dash", true);
            nextTimeToDash = Time.time + dashCooldown;
            if (Input.GetAxis("Horizontal") > 0) // Right dash
            {
                trail.Play();
                StartCoroutine(cameraShake.Shake(.1f, .05f));
                rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                dashTimer -= Time.deltaTime;
                //Stop dashing
                if (dashTimer <= 0)
                {
                    trail.Stop();
                    rb.velocity = Vector2.zero;
                    isDashing = false;
                    GetComponent<Animator>().SetBool("dash", false);
                }
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                trail.Play();
                StartCoroutine(cameraShake.Shake(.1f, .05f));
                rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
                dashTimer -= Time.deltaTime;

                //Stop dashing
                if (dashTimer <= 0)
                {
                    trail.Stop();
                    rb.velocity = Vector2.zero;
                    isDashing = false;
                    GetComponent<Animator>().SetBool("dash", false);
                }
            }
            else if(Input.GetAxis("Horizontal") == 0 && transform.localScale.x > 0)
            {
                trail.Play();
                StartCoroutine(cameraShake.Shake(.1f, .05f));
                rb.AddForce(Vector2.right * dashSpeed, ForceMode2D.Impulse);
                dashTimer -= Time.deltaTime;
                //Stop dashing
                if (dashTimer <= 0)
                {
                    trail.Stop();
                    rb.velocity = Vector2.zero;
                    isDashing = false;
                    GetComponent<Animator>().SetBool("dash", false);
                }
            }
            else if (Input.GetAxis("Horizontal") == 0 && transform.localScale.x < 0)
            {
                trail.Play();
                StartCoroutine(cameraShake.Shake(.1f, .05f));
                rb.AddForce(Vector2.left * dashSpeed, ForceMode2D.Impulse);
                dashTimer -= Time.deltaTime;
                //Stop dashing
                if (dashTimer <= 0)
                {
                    trail.Stop();
                    rb.velocity = Vector2.zero;
                    isDashing = false;
                    GetComponent<Animator>().SetBool("dash", false);
                }
            }
        }

        //Springboard
        if (onSpringboard)
        {
            
            springBoard();
        }       

        //Reached top of jump
        if (rb.velocity.y <= 0)
        {
            GetComponent<Animator>().SetBool("isJumping", false);
        }

        // Falling
        if(rb.velocity.y < -2 && !onWall)
        {
            jump = true;
            GetComponent<Animator>().SetBool("isFalling", true);
        }
        // End of fall
        else if (rb.velocity.y > -2 && rb.velocity.y <= 0)
        {
            GetComponent<Animator>().SetBool("isFalling", false);
        }

        //Adjusts speed on platform
        if (onPlatform && mp.platformType == MovingPlatform.PlatformType.LeftToRight)
        {
            platformSpeed = mp.platformSpeed;
            Vector3 platformMovement = new Vector3(platformSpeed, 0f, 0f);
            transform.position += platformMovement * Time.fixedDeltaTime;
        }
        if (onPlatform && mp.platformType == MovingPlatform.PlatformType.UpDown)
        {
            platformSpeed = mp.platformSpeed;
            Vector3 platformMovement = new Vector3(0f, platformSpeed, 0f);
            transform.position += platformMovement * Time.fixedDeltaTime;
        }

        if (Grounded)
        {
            dashCount = 0;
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.fixedDeltaTime * moveSpeed;

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (Grounded)
            {
                dust.Play();

            }
            //audioManager.Play("Run");
            platformSpeed -= platformSpeed;
            if (!onWall) transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Animator>().SetFloat("Speed", moveSpeed);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            if (Grounded)
            {
                dust.Play();
            }
            //audioManager.Play("Run");

            platformSpeed -= platformSpeed;
            if (!onWall) transform.localScale = new Vector3(-1, 1, 1);
            GetComponent<Animator>().SetFloat("Speed", moveSpeed);

        }
        else 
        {
            //audioManager.Stop("Run");
            GetComponent<Animator>().SetFloat("Speed", 0);  
        }



    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        jump = true;
        //rb.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        AudioManager.instance.Play("Jump");
        jumpDust.Play();
        Grounded = false;
    }

    void WallSlide()
    {
        float x = Input.GetAxisRaw("Horizontal");

        GetComponent<Animator>().SetBool("onWall", true);
        
        rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
        if (Grounded)
        {
            onWall = false;
        }
    }

    void Land()
    {
        landDust.Play();
        AudioManager.instance.Play("Land");
    }

    void climbLadder()
    {
        GetComponent<Animator>().SetBool("OnLadder", true);
        Vector3 moveLadder = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
        {
            GetComponent<Animator>().SetFloat("climbSpeed", moveSpeed * 0.7f);
        }
        else
        {
            GetComponent<Animator>().SetFloat("climbSpeed", 0);
        }
        transform.position += moveLadder * Time.fixedDeltaTime * (moveSpeed * 0.7f);
    }

    void springBoard()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(new Vector2(0f, launch), ForceMode2D.Impulse);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Springboard"))
        {
            launch = collision.gameObject.GetComponent<Springboard>().launchUp;
            onSpringboard = true;
            //collision.gameObject.GetComponent<Animator>().SetBool("onSpringboard", true);
            collision.gameObject.GetComponent<Animator>().Play("Springboard");
            GetComponent<Animator>().SetBool("isJumping", true);
        }

        if (collision.CompareTag("Key"))
        {
            AudioManager.instance.Play("key");
            kh.hasKey = true;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Finish"))
        {
            if (SceneManager.GetActiveScene().name == "Level01")
            {
                LevelManager.levelManager.level = 2;
            }
            else if (SceneManager.GetActiveScene().name == "Level02") LevelManager.levelManager.level = 3;
            GetComponent<Animator>().SetFloat("Speed", 0);
            this.enabled = false;
            transition.Play("Death");
            finish.SetActive(true);
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Springboard")
        {
            onSpringboard = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            onWall = true;
            Flip();
        }

        if (collision.collider.tag == "Spikes")
        {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<PlayerHealth>().TakeDamage();
        }
        if (collision.collider.tag == "Door")
        {

            if (kh.hasKey)
            {
                StartCoroutine(collision.gameObject.GetComponent<UnlockDoor>().OpenDoor());
            }
        }
        if (collision.gameObject.layer == 6)
        {
            mp = collision.gameObject.GetComponent<MovingPlatform>();
            onPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            GetComponent<Animator>().SetBool("onWall", false);
            onWall = false;

        }

        if (collision.gameObject.layer == 6)
        {
            onPlatform = false;
        }
    }
}

