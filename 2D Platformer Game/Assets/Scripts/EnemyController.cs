using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // PUBLIC //

    public float moveSpeed; //movement speed of the enemy

    public Transform leftMovePoint, rightMovePoint; //two points in the world between which the player moves

    public SpriteRenderer enemySpriteRenderer; //SpriteRenderer of the enemy
    
    public float moveTime, waitTime; //time for movement and time to wait between movements

    //health
    public int currentHealth;
    public int maxHealth;
    //death effect of the enemy
    public GameObject deathEffect;
    //collectible which will be dropped (cherry or fire)
    public GameObject collectibleCherry;
    public GameObject collectibleFire;
    //threshold (chance) of dropping the collectible
    [Range(0, 100)] public float chanceToDropCollectible;

    // PRIVATE //

    private bool isMovingRight; //denotes if the player is moving right

    private Rigidbody2D rigidBody; //Rigidbody component of the enemy

    private float moveTimeCounter, waitTimeCounter; //used to count moveTime and waitTime

    private Animator enemyAnimator; //Animator component of the enemy

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        enemyAnimator = GetComponent<Animator>();

        //set LeftPoint and RightPoint to not be a child of Enemy Frog when the game starts
        //this is done so LeftPoint and RightPoint don't retain their relative positions towards the Enemy Frog
        //otherwise the Enemy Frog would just chase the left point indefinitely
        leftMovePoint.parent = null;
        rightMovePoint.parent = null;

        isMovingRight = true;

        moveTimeCounter = moveTime;

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if(moveTimeCounter <= 0)
        {
            //moveTimeCounter is <=0, which means that waitTimeCounter must be > 0
            //THE ENEMY IS WAITING
            waitTimeCounter -= Time.deltaTime;

            if(waitTimeCounter <= 0)
            {
                //THE ENEMY HAS STOPPED WAITING, SWITCH TO MOVING MODE

                //randomize moveTime
                moveTimeCounter = Random.Range(moveTime * 0.75f, moveTime * 1.25f);
            }

            return;
        }

        moveTimeCounter -= Time.deltaTime;

        //set animator flag for animation transition
        enemyAnimator.SetBool("isMoving", true);

        if(isMovingRight)
        {
            //MOVING RIGHT
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);

            enemySpriteRenderer.flipX = true; //flip the sprite to face the correct way

            if (transform.position.x > rightMovePoint.position.x)
            {
                isMovingRight = false;
            }
        }
        else
        {
            //MOVING LEFT
            rigidBody.velocity = new Vector2(-moveSpeed, rigidBody.velocity.y);

            enemySpriteRenderer.flipX = false; 

            if (transform.position.x < leftMovePoint.position.x)
            {
                isMovingRight = true;

            }

        }


        if(moveTimeCounter <= 0)
        {
            //THE ENEMY HAS STOPPED MOVING, SWITCH TO WAITING MODE

            //randomize the waitTime
            waitTimeCounter = Random.Range(waitTime * 0.75f, waitTime * 1.25f);

            //stop the enemy, set velocity to 0 on X axis
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);

            //set animator flag for animation transition
            enemyAnimator.SetBool("isMoving", false);
        }


    }

    private GameObject ChooseDropCollectible()
    {
        //generate a random number between 0 and 100
        //if it's <= 50, top a cherry, otherwise drop a fire
        float dropNumber = Random.Range(0, 100f);

        if (dropNumber <= 50)
        {
            return collectibleCherry;
        }
        else
        {
            return collectibleFire;
        }
    }

    public void DamageEnemy()
    {
        currentHealth--;

        if(currentHealth == 0)
        {
            //destroy enemy
            //set object as false
            gameObject.SetActive(false);
            //instantiate a death effect
            //Instantiate creates a copy (gameObject passed as first arg, position and rotation as 2nd and 3rd)
            Instantiate(deathEffect, transform.position, transform.rotation);

            //drop the collectible if the threshod has been achieved
            float dropChance = Random.Range(0, 100f);

            if (dropChance <= chanceToDropCollectible)
            {
                //drop collectible
                Instantiate(ChooseDropCollectible(), transform.position, transform.rotation);
            }
        }
    }
}
