using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    // PUBLIC //
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        //destroy the mine
        Destroy(gameObject);

        //instantiate the explosion
        Instantiate(explosion, transform.position, transform.rotation);

        //play explode SFX
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.EnemyExplode);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Explode();

            //damage the Player
            PlayerHealthController.instance.DamagePlayer();
        }
    }




}
