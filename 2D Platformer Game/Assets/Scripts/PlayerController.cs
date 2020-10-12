using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //PUBLIC//
    public float moveSpeed; 
    public float jumpForce;
    public Rigidbody2D rigidBody; //player's rigid body component - set through editor


    public Transform groundCheckPoint; //Transform object of Ground Point at the player's feet
    public LayerMask whatIsGround; //check what layer is Ground


    //PRIVATE//
    private bool isGrounded;//denotes if the player is touching the ground
    private bool canDoubleJump; //denotes if the player is allowed a second jump

    private Animator animator;//Animator component attached to the player
    private SpriteRenderer spriteRenderer; //SpriteRenderer component attached to the player

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
        rigidBody.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), rigidBody.velocity.y);
       
        //does an overlap and checks if any other object collides with the circle
        //creates a circle at groundCheckPoint.position of diameter .2f and checks if it collides with any object
        //on Ground layer (whatIsGround)
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if(isGrounded) //only if grounded
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


        //set the Animator's parameters
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("moveSpeed", Mathf.Abs(rigidBody.velocity.x));

        //change direction of the player depending on if he moves forward/backwards
        //ONLY if x component of velocity is !=0 (so the player stays facing the desired direction when stopped)
        if (rigidBody.velocity.x != 0)
        {
            spriteRenderer.flipX = (rigidBody.velocity.x < 0);
        }
        
    }
}
