using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    //Knockingback the character
    public int knockBackForce;
    //Player proporties
    Rigidbody2D body2D;
    public float playerSpeed;
    BoxCollider2D box2d;
    CircleCollider2D cir2D;

    //Jumping
    public float jumpPower;
    public float doubleJumpPower;
    public bool canDoubleJump;
    public bool canDamage;
    public GameObject stings;
    public bool isGrounded;
    Transform groundCheck;
    const float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    //Animator Controller
    Animator playerAnim;

    //Facing
    bool facingRight = true;

    //Player Health
    public int maxPlayerHealth=100;
    public int currentPlayerHealth;
    public bool isHurt;
    

    //Kill player
    public bool isDead;
    public float deadForce;

    //Player points
    public int currentPoints;
    public int point = 10;

    //GameManager
    GameManager gameManager;

    //CheckPoint
    public GameObject startPosition;
    GameObject checkPoint;

    //Sound
   AudioSource auSource;
   AudioClip ac_Jump;
   AudioClip ac_Hurt;
   AudioClip ac_PickUpCoin;
   AudioClip ac_Death;
   AudioClip ac_Shot;
    AudioClip ac_Healed;
    //Shoot
    Transform firePoint;
    GameObject bullet;
    public float bulletSpeed;

    void Start()
    {
        transform.position = startPosition.transform.position;
        stings.SetActive(false);

        //Getting the colliders
        box2d = GetComponent<BoxCollider2D>();
        cir2D = GetComponent<CircleCollider2D>();


        //Getting the animator component
        playerAnim = GetComponent<Animator>();
        //Rigidbody Settings
        body2D = GetComponent<Rigidbody2D>();
        body2D.gravityScale = 1;
        body2D.freezeRotation = true;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //Find GroundCheck
        groundCheck = transform.Find("GroundCheck");
        //Equals currentPlayerHealth as maxPlayerHealth
        
        currentPlayerHealth = maxPlayerHealth;

        //GameManager
        gameManager=FindObjectOfType<GameManager>();

        //Load Sound Effects
        auSource=GetComponent<AudioSource>();
        ac_Jump = Resources.Load("SoundEffects/Jump") as AudioClip;
        ac_Hurt = Resources.Load("SoundEffects/Hit_Hurt") as AudioClip;
        ac_PickUpCoin = Resources.Load("SoundEffects/Pickup_Coin") as AudioClip;
        ac_Death = Resources.Load("SoundEffects/Death") as AudioClip;
        ac_Shot = Resources.Load("SoundEffects/Shot") as AudioClip;
        ac_Healed = Resources.Load("SoundEffects/Healed") as AudioClip;

        //Shooting
        firePoint = transform.Find("FirePoint");
        bullet = Resources.Load("Bullet") as GameObject;
    }

    void Update()
    {
        UpdateAnimations();
        ReduceHealth();
        isDead = currentPlayerHealth < 1;


        //This code doesnt let currentHealth to become more than max health
        if (currentPlayerHealth>maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
        if (isDead)
        {
        KillPlayer();
        }
        if (transform.position.y<=-18)
        {
            isDead = true;
        }
    }
    // Works independence of framerate.
    void FixedUpdate()
    {
        //Checking if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Setting up the Player's horizontal movement
        float h = Input.GetAxis("Horizontal");
        body2D.velocity= new Vector2(h*playerSpeed,body2D.velocity.y);
        Flip(h);
        if (isGrounded)
        {
            canDamage = false;
        }
        if (canDamage)
        {
            stings.SetActive(true);
        }
        else
        {
            stings.SetActive(false);
        }
    }

    public void Jump()
    {
        //Jumping function
        auSource.PlayOneShot(ac_Jump);
        auSource.pitch = Random.Range(0.8f, 1.1f);
        body2D.AddForce(new Vector2(0, jumpPower));
        
    }
    public void DoubleJump()
    {
        //Jumping but instantly forces through y axis
        auSource.PlayOneShot(ac_Jump);
        auSource.pitch = Random.Range(0.8f, 1.1f);
        body2D.AddForce(new Vector2(0, doubleJumpPower), ForceMode2D.Impulse);
        canDamage = true;
    }
    void Flip(float h)
    {
        if (h>0 && !facingRight || h<0 && facingRight)
        {
            facingRight = !facingRight;
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale=theScale;
        }
    }
    void UpdateAnimations()
    {
        playerAnim.SetFloat("VelocityX", Mathf.Abs(body2D.velocity.x));
        playerAnim.SetBool("isGrounded", isGrounded);
        playerAnim.SetFloat("VelocityY",body2D.velocity.y);
        if (true)
        {

        }
        playerAnim.SetBool("isDead", isDead);
        if (isHurt && !isDead)
        {
        playerAnim.SetTrigger("isHurt");

        }
    }

    
    void ReduceHealth()
    {
        if (isHurt)
        {
            auSource.PlayOneShot(ac_Hurt);
            playerSpeed = playerSpeed/100f;
            Invoke("StunOver", 0.7f);
            isHurt = false;

            if (facingRight&&!isGrounded)
            
                body2D.AddForce(new Vector2(-knockBackForce, 500), ForceMode2D.Force);
            
            else if (!facingRight && !isGrounded)
            
                body2D.AddForce(new Vector2(knockBackForce,500),ForceMode2D.Force);


            if (facingRight && isGrounded)
            
                body2D.AddForce(new Vector2(-knockBackForce, 0), ForceMode2D.Force);
            
            else if (!facingRight && isGrounded)
            
                body2D.AddForce(new Vector2(knockBackForce, 0), ForceMode2D.Force);
            
        }
    }


    //Killing player function

    void KillPlayer()
    {
        if (isDead)
        {
            auSource.PlayOneShot(ac_Death);
            auSource.volume = 0.3f;
            isHurt = false;
            body2D.AddForce(new Vector2(0, deadForce),ForceMode2D.Force);
            body2D.drag += Time.deltaTime * 10;
            deadForce -= Time.deltaTime * 10;
            body2D.constraints = RigidbodyConstraints2D.FreezePositionX;
            box2d.enabled = false;
            cir2D.enabled = false;
        }
    }
    public void RecoverPlayer()
    {

        if (checkPoint!=null)
        {
            transform.position = checkPoint.transform.position;
        }
        else
        {
            transform.position = startPosition.transform.position;
        }
        deadForce = 4.5f;
        body2D.gravityScale = 1;
        body2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        body2D.constraints = RigidbodyConstraints2D.None;
        body2D.freezeRotation = true;
        body2D.simulated= true;
        body2D.drag = 0;
        currentPlayerHealth = maxPlayerHealth;
        box2d.enabled = true;
        cir2D.enabled=true;
    }
 
    void StunOver()
    {
        playerSpeed = playerSpeed * 100f;
    }

    public void Shooting()
    {
        GameObject b = Instantiate(bullet) as GameObject ;
        b.transform.position=firePoint.transform.position;
        b.transform.rotation=firePoint.transform.rotation;
        auSource.PlayOneShot(ac_Shot);
        auSource.pitch = Random.Range(0.8f, 1.1f);


        if (transform.localScale.x<0)
        {
            b.GetComponent<Shooting>().bulletSpeed *= -1;
            b.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            b.GetComponent<Shooting>().bulletSpeed *= 1;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Coin")
        {
            auSource.PlayOneShot(ac_PickUpCoin);
            //auSource.pitch = Random.Range(0.8f, 1.1f);
            currentPoints+=point;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag=="CheckPoint")
        {
            checkPoint = other.gameObject;

        }
        if (other.gameObject.tag=="LevelFinished")
        {
            SceneManager.LoadScene(2);
        }
        if (other.gameObject.tag=="Heal")
        {
            auSource.PlayOneShot(ac_Healed);
            currentPlayerHealth = currentPlayerHealth + 20;
            Destroy(other.gameObject);  
        }

    }
}
