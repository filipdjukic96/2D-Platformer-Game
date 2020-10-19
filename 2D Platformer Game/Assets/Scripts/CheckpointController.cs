using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    //PUBLIC//

    //instance of CheckpointController (SINGLETON)
    public static CheckpointController instance; 

    public Vector3 spawnPoint; //Player's spawn point

    //PRIVATE//

    //array of all checkpoints
    private Checkpoint[] checkpoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //find all checkpoints in the scene when the level starts
        checkpoints = FindObjectsOfType<Checkpoint>();

        //set spawnPoint to Player's start point
        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //go through the checkpoints array and call ResetCheckpoints on every member
    public void ResetCheckpoints()
    {
        foreach(Checkpoint checkpoint in checkpoints)
        {
            checkpoint.ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
