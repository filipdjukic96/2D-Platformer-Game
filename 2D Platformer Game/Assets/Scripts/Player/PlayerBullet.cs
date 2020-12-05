using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // PUBLIC //
    public float bulletSpeed;

    //effect of bullet destroy
    public GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        //play shoot sound effect on Start
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.Shot);
    }

    // Update is called once per frame
    void Update()
    {
        //the bullet moves depending on its scale over the X axis
        //if the scale is 1 - the bullet is facing to the right and its X component of velocity should increase by bulletSpeed
        //if the scale is -1 - the bullet is facing to the left and its X component of velocity should decrease by bulledSpeed

        transform.position += new Vector3(transform.localScale.x * bulletSpeed * Time.deltaTime, 0f, 0f);

        //check if the bullet has passed the left or right edge of the screen
        //if so, destroy the bullet
        if(transform.position.x < KillPlayer.instance.leftEndPoint.position.x || transform.position.x > KillPlayer.instance.rightEndPoint.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.tag)
        {

            case "Enemy":
                HitEnemy(other);
                break;
            case "TankBoss":
                HitTankBoss(other);
                break;
            case "KnightBoss":
                HitKnightBoss(other);
                break;
            case "MonsterBoss":
                HitMonsterBoss(other);
                break;
            default:
                HitOther(other);
                break;
        }

        
    }

    private void HitOther(Collider2D other)
    {
        //only do something if the bullet has collided with anything other than the Player (and EnemyObject bcs of two colliders on enemies)
        //this is done to make sure the bullet isn't destroyed when the Player shoots while running
        //because then the bullet might collide with the player
        if (!other.CompareTag("Player") && !other.CompareTag("EnemyObject"))
        {
            Debug.Log("Other hit");

            DestroyBullet();
        }
    }

    private void HitEnemy(Collider2D other)
    {
        Debug.Log("Enemy hit");

        //bullet hit an enemy, damage the enemy

        //the Box Collider 2D collided with the Box Collider of the enemy sprite


        //!!!!! cannot damage the other.gameObject only because it would only damage the enemy sprite but not the whole object itself !!!!!
        //this is why we need to damage the parent object
        //if it has an EnemyController attached, it's a real enemy
        if (other.transform.parent.gameObject.TryGetComponent<EnemyHealthController>(out var enemyHealthController))
        {
            Debug.Log("Hit enemy with bullet");
            enemyHealthController.DamageEnemy();
        }
        else
        {
            //unknown enemy type
            Debug.Log("Enemy has no health controller");
        }

        //destroy the bullet
        DestroyBullet();
    }

    private void HitTankBoss(Collider2D other)
    {
        Debug.Log("Hit Tank Boss");

        if (other.transform.gameObject.TryGetComponent<BossTankHitBox>(out var bossHitBox))
        {
            Debug.Log("Hit Tank Boss with bullet");
            bossHitBox.TakeHit();
        }
        else
        {
            //unknown enemy type
            Debug.Log("Tank Boss has no hit box controller!");
        }


        //destroy the bullet
        DestroyBullet();
    }

    private void HitKnightBoss(Collider2D other)
    {
        Debug.Log("Hit Knight Boss");

        if (other.transform.gameObject.TryGetComponent<BossKnightController>(out var bossController))
        {
            Debug.Log("Hit Knight Boss with bullet");
            bossController.TakeHit();
        }
        else
        {
            //unknown enemy type
            Debug.Log("Knight Boss has no hit box controller!");
        }


        //destroy the bullet
        DestroyBullet();
    }

    private void HitMonsterBoss(Collider2D other)
    {
        Debug.Log("Hit Monster Boss");

        if (other.transform.gameObject.TryGetComponent<BossMonsterController>(out var bossController))
        {
            Debug.Log("Hit Monster Boss with bullet");
            bossController.TakeHit();
        }
        else
        {
            //unknown enemy type
            Debug.Log("Monster Boss has no hit box controller!");
        }


        //destroy the bullet
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        //instantiate a destroy effect for the bullet
        Instantiate(destroyEffect, transform.position, transform.rotation);

        //at the end, destroy the bullet object
        Destroy(gameObject);
    }
}
