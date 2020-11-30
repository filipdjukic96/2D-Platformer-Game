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
        Moving,
        Defeated
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

    //mine to drop while moving
    public GameObject mine;

    //point from which the mines come out
    public Transform minePoint;

    //time between mines
    public float timeBetweenMines;



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

    //boss' hit box
    public GameObject hitBox;



    [Header("Health Reduction")]
   
    //how much shots speed up when the boss is hit
    public float shotSpeedUp;

    //how much time between mines speeds up when the boss is hit
    public float mineSpeedUp;
    



    // PRIVATE //

    //denotes if the boss is moving to the right
    private bool moveRight;

    //counter to keep track of time between bullets
    private float shotCounter;

    //hurt time counter
    private float hurtCounter;

    //counter to drop mines
    private float mineCounter;

    //denotes if the boss is defeated
    private bool isDefeated;

    //BossHealthController script attached to this object
    private BossHealthController bossHealthController;

    // Start is called before the first frame update
    void Start()
    {
        currentState = BossStates.Shooting;

        //find boss health controller
        bossHealthController = gameObject.GetComponent<BossHealthController>();
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

        //play boss hit sfx
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.BossHit);

        //remove all existing mines

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();

        if(mines.Length > 0)
        {
            foreach(BossTankMine mine in mines)
            {
                mine.Explode(); //explode the mine
            }
        }

        bossHealthController.TakeHit();

        if(bossHealthController.CurrentHealth() <= 0)
        {
            //boss defeated
            isDefeated = true;
        }
        else
        {
            //speed up bullet shooting and mines

            timeBetweenBulletShots /= shotSpeedUp;

            timeBetweenMines /= mineSpeedUp;
        }
    }

    private void BossShooting()
    {

        //shot counter
        shotCounter -= Time.deltaTime;

        if(shotCounter <= 0)
        {
            //reset the counter
            shotCounter = timeBetweenBulletShots;

            ShootBullet();
        }

    }

    private void ShootBullet()
    {
        //instantiate the bullet
        var newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        //set the bullet's scale to match the Boss's scale
        //so the bullet would face the right direction
        newBullet.transform.localScale = theBoss.localScale;
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

                //reset mine counter
                mineCounter = 0f; //->a mine is dropped instantly

                //check if the boss is defeated
                if(isDefeated)
                {
                    //switch to defeated state - do nothing
                    currentState = BossStates.Defeated;
                }
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


        mineCounter -= Time.deltaTime;

        if(mineCounter <= 0)
        {
            //reset counter
            mineCounter = timeBetweenMines;

            //drop a mine
            Instantiate(mine, minePoint.position, minePoint.rotation);
        }
    }

    private void EndMovement()
    {
        //start shooting
        currentState = BossStates.Shooting;

        //reset shot counter
        shotCounter = 0f; //-> to shoot quicker

        //set the StopMoving trigger in the animator
        bossAnimator.SetTrigger("StopMoving");

        //activate the hit box
        hitBox.SetActive(true);
    }
}
