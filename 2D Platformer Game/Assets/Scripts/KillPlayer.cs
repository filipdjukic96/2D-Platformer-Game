using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    // PUBLIC //
    public static KillPlayer instance;

    //left and right edge of the level
    public Transform leftEndPoint, rightEndPoint;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.instance.KillPlayer();
            //respawn the Player
            LevelManager.instance.RespawnPlayer();
        }
    }
}
