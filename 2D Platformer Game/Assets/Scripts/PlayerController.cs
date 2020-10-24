using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PUBLIC//

    public static PlayerController instance;

    public float moveSpeed; 
    public float jumpForce;
    public Rigidbody2D rigidBody; //player's rigid body component - set through editor


    public Transform groundCheckPoint; //Transform object of Ground Point at the player's feet
    public LayerMask whatIsGround; //check what layer is Ground

    //knockback
    public float knockBackDuration;
    public float knockBackForce;


    //bouncing off the enemy
    public float bounceForce; //how much the player will bounce

    //PRIVATE//
    private bool isGrounded;//denotes if the player is touching the ground
    private bool canDoubleJump; //denotes if the player is allowed a second jump

    private Animator animator;//Animator component attached to the player
    private SpriteRenderer spriteRenderer; //SpriteRenderer component attached to the player

    //counter for counting knockback
    private float knockBackCounter;

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
    }

    // Update is called once per frame
    void Update()
    {
        //if knockback is active, no input is allowed
        if(knockBackCounter <= 0)
        {
            rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);

            //does an overlap and checks if any other object collides with the circle
            //creates a circle at groundCheckPoint.position of diameter .2f and checks if it collides with any object
            //on Ground layer (whatIsGround)
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded) //only if grounded
                {
                    rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                    canDoubleJump = true;
                }
                else
                {
                    if (canDoubleJump)
                    {
                        rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
                        canDoubleJump = false;
                    }
                }

            }

            //change direction of the player depending on if he moves forward/backwards
            //ONLY if x component of velocity is !=0 (so the player stays facing the desired direction when stopped)
            if (rigidBody.velocity.x != 0)
            {
                spriteRenderer.flipX = (rigidBody.velocity.x < 0);
            }

        }
        else
        {
            knockBackCounter -= Time.deltaTime;

            //knock back player on X axis by knockBackForce depending on the direction he's facing
            if (!spriteRenderer.flipX)
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



        //set the Animator's parameters
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));

        
        
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackDuration;
        //stop player on X axis, make him jump on Y axis 
        rigidBody.velocity = new Vector2(0f, knockBackForce);
        //activate the trigger 'hurt' in Player animator for Player_Hurt animation 
        animator.SetTrigger("hurt");
    }


    public void SetPlayerPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public void Bounce()
    {
        rigidBody.velocity = new Vector2(rigidBody.velocity.x, bounceForce);
    }
}
