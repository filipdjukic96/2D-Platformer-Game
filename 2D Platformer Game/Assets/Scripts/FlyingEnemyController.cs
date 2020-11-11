using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    // PUBLIC //

    public Transform[] points; //points between which the eagle will move
    public float moveSpeed;

    public SpriteRenderer spriteRenderer; //sprite renderer of the enemy eagle

    public float distanceToAttackPlayer; //at which distance the enemy attacks the Player
    public float chaseSpeed; //speed with which the enemy will attack

    public float waitAfterAttack; //how long to wait after an attack

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
    private int currentPointIndex;

    //position the eagle will go towards in attack mode
    private Vector3 attackTarget;

    //attack counter
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        //loop through all points and make sure they're not a child of the enemy
        //otherwise the enemy will always chase the first point (as it's always the same distance from the enemy)
        foreach(Transform point in points)
        {
            point.transform.parent = null;
        }

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCounter > 0)
        {
            //if the enemy has attacked and is waiting, just pass the time
            attackCounter -= Time.deltaTime;
        }
        else
        {
            //otherwise, allow movement or attack
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) > distanceToAttackPlayer)
            {
                //reset attack target
                attackTarget = Vector3.zero;

                MoveFlyingEnemy();

                //set which direction the enemy should be facing relative to the current point
                SetFlyingEnemyDirection(points[currentPointIndex]);
            }
            else
            {
                //ATTACKING THE PLAYER
                AttackPlayer();

                //set which direction the enemy should be facing relative to the Player
                SetFlyingEnemyDirection(PlayerController.instance.transform);
            }
        }

    }



    private void MoveFlyingEnemy()
    {
        //move the enemy to current point
        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex].position, moveSpeed * Time.deltaTime);

        //if the enemy has reached the current point
        if (Vector3.Distance(transform.position, points[currentPointIndex].position) <= .05f)
        {
            //go to next point
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }

    private void SetFlyingEnemyDirection(Transform desiredPosition)
    {
        //if the enemy is left of the desired position, it needs to be facing to the right (flipX)
        if(transform.position.x < desiredPosition.position.x)
        {
            spriteRenderer.flipX = true;
            //transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //vice-versa, if the enemy is right of the desired position, it needs to be facing to the left (no flip)
        else if (transform.position.x > desiredPosition.position.x)
        {
            spriteRenderer.flipX = false;
            //transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void AttackPlayer()
    {
        if (attackTarget == Vector3.zero)
        {
            //set attack target to Player's position
            attackTarget = PlayerController.instance.transform.position;
        }

        //move towards attack position
        transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

        //check if attack is successful
        if(Vector3.Distance(transform.position, attackTarget) <= .1f)
        {
            //set wait time after an attack
            attackCounter = waitAfterAttack;

            //reset attack target so the enemy will look for the Player again
            attackTarget = Vector3.zero;
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

        if (currentHealth == 0)
        {
            //destroy enemy
            //set object as false
            gameObject.SetActive(false);
            //instantiate a death effect
            //Instantiate creates a copy (gameObject passed as first arg, position and rotation as 2nd and 3rd)
            Instantiate(deathEffect, transform.position, transform.rotation);

            //play enemy destroyed sound effect
            AudioManager.instance.PlaySFX(AudioManager.SoundEffects.EnemyExplode);

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
