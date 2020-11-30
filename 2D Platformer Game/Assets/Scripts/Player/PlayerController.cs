using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PUBLIC//

    public static PlayerController instance;

    [Header("Movement")]
    public float moveSpeed; 
    public float jumpForce;
    public Rigidbody2D rigidBody; //player's rigid body component - set through editor


    public Transform groundCheckPoint; //Transform object of Ground Point at the player's feet
    public LayerMask whatIsGround; //check what layer is Ground

    [Header("Knockback")]
    //knockback
    public float knockBackDuration;
    public float knockBackForce;
    public Collider2D stompBoxCollider;

    [Header("Shooting")]
    //fire bullet that the Player can fire
    public GameObject fireBullet;
    //transform object of the point where the fire bullet is fired
    public Transform firePoint;

    [Header("Bouncing")]
    //bouncing off the enemy
    public float bounceForce; //how much the player will bounce

    [Header("Level End")]
    public bool stopInput; //inuput disabled when the level ends

    [Header("Particle Effects")]
    public ParticleSystem footstepsEffect;
    public ParticleSystem impactEffect;


    //PRIVATE//
    private bool isGrounded;//denotes if the player is touching the ground
    private bool canDoubleJump; //denotes if the player is allowed a second jump

    private Animator animator;//Animator component attached to the player
    private SpriteRenderer spriteRenderer; //SpriteRenderer component attached to the player

    //counter for counting knockback
    private float knockBackCounter;

    //for particle generation speed changes 
    private ParticleSystem.EmissionModule footEmission;

    //denotes if the Player was on the ground in the previous frame
    private bool wasOnGround;
  
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //find the component of type Animator that is attached to the same object this script is attached to
        //script automatically recognizes
        animator = GetComponent<Animator>();
        //find the component of type SpriteRenderer that is attached to the same object this script is attached to
        spriteRenderer = GetComponent<SpriteRenderer>();

        //access the emission
        footEmission = footstepsEffect.emission;
    }

    // Update is called once per frame
    void Update()
    {
        //if the game is paused, don't allow input for anything
        if(!PauseMenu.instance.isPaused && !stopInput)
        { 

            //if knockback is active, no input is allowed
            if(knockBackCounter <= 0)
            {
                //MOVING THE PLAYER
                rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);

                //does an overlap and checks if any other object collides with the circle
                //creates a circle at groundCheckPoint.position of diameter .2f and checks if it collides with any object
                //on Ground layer (whatIsGround)
                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }

                //change direction of the player depending on if he moves forward/backwards
                //ONLY if X component of velocity is !=0 (so the player stays facing the desired direction when stopped)
                if (rigidBody.velocity.x != 0)
                {
                    SetPlayerDirection();
                }


                //if the Player wants to shoot
                //if the left mouse button is clicked
                //and if the game ins't paused
                if (Input.GetMouseButtonDown(0))
                {
                    ShootBullet();
                }

            }
            else
            {
                knockBackCounter -= Time.deltaTime;

                KnockBackPlayer();

                if(knockBackCounter <= 0)
                {
                    //ENABLE THE PLAYER'S STOMPBOX COLLIDER
                    //THE PLAYER IS NO LONGER IN KNOCKBACK
                    stompBoxCollider.enabled = true;
                }
            }

        }

        SetParticleEffect();

        SetAnimatorParameters();
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackDuration;
        //stop player on X axis, make him jump on Y axis 
        rigidBody.velocity = new Vector2(0f, knockBackForce);
        //activate the trigger 'hurt' in Player animator for Player_Hurt animation 
        animator.SetTrigger("hurt");

        //DISABLE STOMPBOX COLLIDER WHILE THE PLAYER IS IN KNOCKBACK
        //THIS IS DONE SO THE PLAYER'S STOMPBOX COLLIDER WOULD NOT ACTIVATE WHEN HE'S HIT BY AN ENEMY FROM ABOVE
        stompBoxCollider.enabled = false;
    }


    public void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void Bounce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
        //play Player jump sound effect (used also for bouncing)
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.PlayerJump);
    }

    private void Jump()
    {
        if (isGrounded) //only if grounded
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
            canDoubleJump = true;
            //play Player jump sound effect
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.PlayerJump);
        }
        else
        {
            if (canDoubleJump)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                canDoubleJump = false;
                //play Player jump sound effect
                AudioManager.instance.PlaySFX(AudioManager.SoundEffects.PlayerJump);
            }
        }
    }

    private void SetPlayerDirection()
    {
        // NOT USED //
        //spriteRenderer.flipX = (rigidBody.velocity.x < 0); - not used anymore, not applicable since the Player can shoot

        //if the X component of velocity is > 0 - the player is facing to the right
        if (rigidBody.velocity.x > 0)
        {
            //the scale is (1,1,1)
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            //the scale is (-1,1,1)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void KnockBackPlayer()
    {
        //knock back player on X axis by knockBackForce depending on the direction he's facing
        if (transform.localScale.x == 1)
        {
            //facing right - knock back to the left
            rigidBody.velocity = new Vector2(-knockBackForce, rigidBody.velocity.y);
        }
        else
        {
            //facing left - knock back to the right
            rigidBody.velocity = new Vector2(knockBackForce, rigidBody.velocity.y);
        }
    }

    private void SetParticleEffect()
    {
        //SHOWING FOOTSTEPS EFFECT

        //if the player is moving and is on the ground
        if (Input.GetAxisRaw("Horizontal") != 0 && isGrounded)
        {
            footEmission.rateOverTime = 35f;
        }
        else
        {
            footEmission.rateOverTime = 0f; //stop spawning particles
        }


        //SHOWING IMPACT EFFECT

        //TODO: commented for now until the PUFF effect is created
        
        if(!wasOnGround && isGrounded)
        {
            //the Player just hit the ground

            //activate the impact effect
            impactEffect.gameObject.SetActive(true);
            impactEffect.Stop();

            //move the impact effect at the Player's feet (where the footsteps effect is located)
            impactEffect.transform.position = footstepsEffect.transform.position;

            //play the impact effect
            impactEffect.Play();
        }

        //update wasOnGround flag
        wasOnGround = isGrounded;
        
    }

    private void SetAnimatorParameters()
    {
        //set the Animator's parameters
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));
    }

    private void ShootBullet()
    {
        //check if the Player has any fire pickups collected
        if (LevelManager.instance.fireScoreCollected > 0)
        {
            //allowed to shoot
            //instantiate the bullet at the position of the firePoint
            var newBullet = Instantiate(fireBullet, firePoint.position, firePoint.rotation);

            //set the bullet's scale to match the Player's scale
            //so the bullet would face the right direction
            newBullet.transform.localScale = transform.localScale;

            //decrease the fire pickups collected 
            LevelManager.instance.RemoveFireScore();

        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //if the Player has landed on a platform
        //make the Player a child of the platform
        if(other.gameObject.CompareTag("Platform"))
        {
            transform.parent = other.transform; //set Player as child of Platform object
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //if the Player has jumped away from the platform
        //the Platform object should no longer be the parent of the Player object
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.parent = null; //Player no longer has a parent
        }
    }

}
