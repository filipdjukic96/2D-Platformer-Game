using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //PUBLIC//

    //SpriteRenderer attached to this object
    public SpriteRenderer spriteRenderer;

    //sprites for ACTIVATED/DEACTIVATED CHECKPOINTS
    public Sprite checkpointOn;
    public Sprite checkpointOff;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //automatically called by Unity when the Player enters the trigger area
    private void OnTriggerEnter2D(Collider2D other)
    {
        //make sure the Player has triggered the trigger area
        if(other.tag == "Player")
        {
            //deactivate all checkpoints in the scene
            CheckpointController.instance.ResetCheckpoints();
            //update the current spawn point of the player
            CheckpointController.instance.SetSpawnPoint(transform.position);

            //activate current checkpoint
            //set the ACTIVATED checkpoint sprite
            spriteRenderer.sprite = checkpointOn;
        }
    }

    //DEACTIVATE the checkpoint
    public void ResetCheckpoint()
    {
        spriteRenderer.sprite = checkpointOff;
    }
}
