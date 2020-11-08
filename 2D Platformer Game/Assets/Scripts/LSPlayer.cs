using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    // PUBLIC //

    public MapPoint currentPoint;

    public float moveSpeed = 15f; //how fast the player will move



    // PRIVATE //

    //is the level loading
    private bool levelLoading;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //move towards the current point gradually
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        //register the player's input ONLY if he's at the point where he's moving to
        // and if a level is not loading
        if (Vector3.Distance(transform.position, currentPoint.transform.position) >= .1f || levelLoading)
        {
            return;
        }


        if (Input.GetAxisRaw("Horizontal") > 0.5f) //if the player pressed right
        {
            //check if there's a MapPoint right
            if(currentPoint.right != null)
            {
                SetNextPoint(currentPoint.right);
            }
        }

        if(Input.GetAxisRaw("Horizontal") < - 0.5f) //if the player pressed left
        {
            if(currentPoint.left != null)
            {
                SetNextPoint(currentPoint.left);
            }
        }

        if (Input.GetAxisRaw("Vertical") > 0.5f) //if the player pressed up
        {
            //check if there's a MapPoint right
            if (currentPoint.up != null)
            {
                SetNextPoint(currentPoint.up);
            }
        }

        if (Input.GetAxisRaw("Vertical") < - 0.5f) //if the player pressed right
        {
            //check if there's a MapPoint right
            if (currentPoint.down != null)
            {
                SetNextPoint(currentPoint.down);
            }
        }

        //if the current point is a level and it's not locked
        if(currentPoint.isLevel && !currentPoint.isLocked && currentPoint.levelToLoad != "")
        {
            //display level info
            LSUIController.instance.ShowLevelInfo(currentPoint);

            //check if the Player has pressed the JUMP button
            //load the level
            if(Input.GetButtonDown("Jump"))
            {
                levelLoading = true;
                LSManager.instance.LoadLevel();
            }
        }

    }


    private void SetNextPoint(MapPoint nextPoint)
    {
        //move to next point
        currentPoint = nextPoint;

        //hide level info panel from previous point
        LSUIController.instance.HideLevelInfo();

        //play sound effect for map movement
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.MapMovement);
    }
}
