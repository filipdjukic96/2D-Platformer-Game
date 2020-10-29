using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // PUBLIC //
    public float bulletSpeed;

    //effect of enemy dying
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //the bullet moves depending on its scale over the X axis
        //if the scale is 1 - the bullet is facing to the right and its X component of velocity should increase by bulletSpeed
        //if the scale is -1 - the bullet is facing to the left and its X component of velocity should decrease by bulledSpeed

        transform.position += new Vector3(transform.localScale.x * bulletSpeed * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the bullet hit an enemy
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit");

            //bullet hit an enemy, damage the enemy

            //CASE I: the Box Collider 2D collided with the Box Collider of the enemy sprite
            //CASE II: the Box Collider 2D collided with the Box Collider of the enemy object

            //if other's parent is != null, that means we collided with the enemy sprite
            if (other.transform.parent != null)
            {
                //CASE I
                //!!!!! cannot damage the other.gameObject only because it would only damage the enemy sprite but not the whole object itself !!!!!
                //this is why we need to damage the parent object
                other.transform.parent.gameObject.GetComponent<EnemyController>().DamageEnemy();
            }
            else //collided with the enemy object
            {
                //CASE II
                //the enemy object was hit, damage it
                other.gameObject.GetComponent<EnemyController>().DamageEnemy();

            }


            //at the end, destroy the bullet
            Destroy(gameObject);

        }
        //only do something if the bullet has collided with anything other than the Player
        //this is done to make sure the bullet isn't destroyed when the Player shoots while running
        //because then the bullet might collide with the player
        else if (!other.CompareTag("Player")) 
        {
            Debug.Log("Other hit");
            //bullet hit something else, instantiate a death effect (bullet explode)
            //bullet's position and rotation are used instead of the hit object's
            //TODO: ADD A SEPARATE BULLET-HIT EFFECT
            //Instantiate(deathEffect, transform.position, transform.rotation);

            //at the end, destroy the bullet
            Destroy(gameObject);
        }

        
    }
}
