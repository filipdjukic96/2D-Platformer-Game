using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankBullet : MonoBehaviour
{
    // PUBLIC //

    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //play bullet shot audio on start
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.Shot);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(transform.localScale.x * (-bulletSpeed) * Time.deltaTime, 0f, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        //play boss bullet hit audio
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.BossImpact);

        //destroy the bullet 
        Destroy(gameObject);
    }
}
