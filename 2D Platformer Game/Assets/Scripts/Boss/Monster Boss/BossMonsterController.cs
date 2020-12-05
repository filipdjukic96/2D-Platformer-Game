using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterController : MonoBehaviour
{
    // PUBLIC //
    public bool isDead;

    // PRIVATE //
    private float timeBtwDamage = 1.5f;
    private Animator animator;

    //BossHealthController script attached to this object
    private BossHealthController bossHealthController;


    private void Start()
    {
        animator = GetComponent<Animator>();

        //find boss health controller
        bossHealthController = gameObject.GetComponent<BossHealthController>();
    }

    private void Update()
    {

        

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

    }

    public void TakeHit()
    {

        bossHealthController.TakeHit();

        var currentHealth = bossHealthController.CurrentHealth();

        if (currentHealth <= 4)
        {
            animator.SetTrigger("stageTwo");
        }

        if (currentHealth <= 0)
        {
            animator.SetTrigger("death");
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                //camAnim.SetTrigger("shake");
                //other.GetComponent<Player>().health -= damage;
            }
        }
    }
}
