using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LSUIController : MonoBehaviour
{
    // PUBLIC //

    public static LSUIController instance;

    //image of the fade screen
    public Image fadeScreenImage;
    public float fadeScreenSpeed;

    //panel displaying level info 
    public GameObject levelInfoPanel;
    public Text levelName;

    //level info labels
    public Text gemsScore, gemsMaxScore;
    public Text timeBest, timeTarget;
    

    // PRIVATE //
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //fade the screen from black to normal at level start
        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            //the fade screen is currently clear, make it black
            fadeScreenImage.color = new Color(fadeScreenImage.color.r,
                                              fadeScreenImage.color.g,
                                              fadeScreenImage.color.b,
                                              Mathf.MoveTowards(fadeScreenImage.color.a, 1f, fadeScreenSpeed * Time.deltaTime)); //alpha to 1f by fadeScreenSpeed

            //check if it's faded
            if (fadeScreenImage.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            //the screen is faded, return it to normal 

            fadeScreenImage.color = new Color(fadeScreenImage.color.r,
                                              fadeScreenImage.color.g,
                                              fadeScreenImage.color.b,
                                              Mathf.MoveTowards(fadeScreenImage.color.a, 0f, fadeScreenSpeed * Time.deltaTime)); //alpha to 0f by fadeScreenSpeed

            //check if it's faded
            if (fadeScreenImage.color.a == 0f)
            {
                shouldFadeFromBlack = true;
            }
        }
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

    public void ShowLevelInfo(MapPoint currentPoint)
    {
        //set level name
        levelName.text = currentPoint.levelName;

        //set gems info
        gemsScore.text = "FOUND: " + currentPoint.gemsScoreCollected;
        gemsMaxScore.text = "IN LEVEL: " + currentPoint.gemsScoreTotal;

        //time info
        if(currentPoint.bestTime == 0)
        {
            timeBest.text = "BEST: ---";
        }
        else
        {
            //F1 means float number with one decimal place
            timeBest.text = "BEST: " + currentPoint.bestTime.ToString("F1") + "s";
        }

        timeTarget.text = "TARGET: " + currentPoint.targetTime + "s";

        
        //show level info panel
        levelInfoPanel.SetActive(true);
    }

    public void HideLevelInfo()
    {
        levelInfoPanel.SetActive(false);
    }
}
