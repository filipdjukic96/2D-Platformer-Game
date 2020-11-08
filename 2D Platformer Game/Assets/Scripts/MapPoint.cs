using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{

    // PUBLIC // 
    
    //references to map points up, right, down and left
    public MapPoint up, right, down, left;
    
    //denotes if this specific MapPoint is a level
    public bool isLevel;

    public bool isLocked;

    //level to load
    public string levelToLoad;

    //level to check if it's unlocked
    public string levelToCheck;

    //level name to display
    public string levelName;

    //gems track
    public int gemsScoreCollected, gemsScoreTotal;
    //time track
    public float bestTime, targetTime;

    //badges
    public GameObject gemBadge;
    public GameObject timeBadge;

    // Start is called before the first frame update
    void Start()
    {
        //if it's a level and has a set level name
        if(isLevel && levelToLoad != "")
        {
            //extract the level info (GEMS + TIME)
            //gems
            if(PlayerPrefs.HasKey(levelToLoad + "_gems"))
            {
                gemsScoreCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");
            }
            //time
            if(PlayerPrefs.HasKey(levelToLoad + "_time"))
            {
                bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");
            }

            //display badges if max score and best time is reached
            if(gemsScoreCollected >= gemsScoreTotal)
            {
                gemBadge.SetActive(true);
            }
            
            if(bestTime != 0 && bestTime <= targetTime)
            {
                timeBadge.SetActive(true);
            }


            //lock the level just in case, then see if it needs to be unlocked
            isLocked = true;

            //if the level that needs to be checked is set
            if (levelToCheck != null)
            {
                //check if the level that needs to be checked is unlocked by searching through PlayerPrefs
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        //if it's unlocked (FINISHED), unlock the current level
                        isLocked = false;
                    }
                }
            }

            //edge case for LEVEL 1 -> both levelToLoad and levelToCheck are the same
            //if they are equal, set isLocked to false (LEVEL 1 is always unlocked)
            if(levelToLoad != null && levelToCheck != null)
            {
                if(levelToLoad == levelToCheck)
                {
                    isLocked = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
