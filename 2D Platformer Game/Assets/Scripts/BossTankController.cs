using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{

    // PUBLIC //

    //boss states
    public enum BossStates
    {
        Shooting,
        Hurt,
        Moving
    };



    [Header("Basic")]

    //reference to the boss object
    public Transform theBoss;

    //boss' animator
    public Animator bossAnimator;

    //current boss state
    public BossStates currentState;

    [Header("Movement")]
    //movement speed
    public float moveSpeed;

    //left and right point of the boss' movement
    public Transform leftPoint, rightPoint;



    [Header("Shooting")]
    //bullet the boss will fire
    public GameObject bullet;

    //boss' fire point
    public Transform firePoint;

    //time between firing shots
    public float timeBetweenBulletShots;



    [Header("Hurt")]
    //time to wait after the boss is hurt
    public float hurtTime;


    // PRIVATE //

    //denotes if the boss is moving to the right
    private bool moveRight;

    //counter to keep track of time between bullets
    private float shotCounter;

    //hurt time counter
    private float hurtCounter;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BossStates.Shooting; 
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case BossStates.Shooting:
                BossShooting();
                break;

            case BossStates.Hurt:
                BossHurt();
                break;

            case BossStates.Moving:
                BossMoving();
                break;

        }


        //testing only
#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif

    }


    public void TakeHit()
    {
        //switch state
        currentState = BossStates.Hurt;
        
        //start hurt time
        hurtCounter = hurtTime;

        //set Hit trigger in the animator
        bossAnimator.SetTrigger("Hit");
    }

    private void BossShooting()
    {

        //fire bullet

        //

    }


    private void BossHurt()
    {
        //count down the hurt time
        if(hurtCounter > 0)
        {
            hurtCounter -= Time.deltaTime;

            if(hurtCounter <= 0)
            {
                currentState = BossStates.Moving;
            }
        }

    }


    private void BossMoving()
    {

        if(moveRight)
        {
            theBoss.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            //if the right point has been reached
            if(theBoss.position.x > rightPoint.position.x)
            {
                moveRight = false; //stop moving to the right

                //change the boss' direction so he faces to the left
                theBoss.localScale = new Vector3(1f, 1f, 1f);

                EndMovement();
            }
        }
        else
        {
            theBoss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);

            //if the left point has been reached
            if (theBoss.position.x < leftPoint.position.x)
            {
                moveRight = true; //start moving to the right

                //change the boss' direction so he faces to the right
                theBoss.localScale = new Vector3(-1f, 1f, 1f);

                EndMovement();
            }
        }

    }

    private void EndMovement()
    {
        //start shooting
        currentState = BossStates.Shooting;

        //reset shot counter
        shotCounter = timeBetweenBulletShots;

        //set the StopMoving trigger in the animator
        bossAnimator.SetTrigger("StopMoving");
    }
}
