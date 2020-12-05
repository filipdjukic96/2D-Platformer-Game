using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMonsterHitBox : MonoBehaviour
{

    // PRIVATE //

    private float timeBtwDamage = 1.5f;

    private BossMonsterController bossController;

    // Start is called before the first frame update
    void Start()
    {
        bossController = gameObject.GetComponent<BossMonsterController>();
    }

    // Update is called once per frame
    void Update()
    {

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && bossController.isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                //damage Player
                PlayerHealthController.instance.DamagePlayer();

                //play boss hit audio
                AudioManager.instance.PlaySFX(AudioManager.SoundEffects.BossImpact);
            }
        }
    }
}
