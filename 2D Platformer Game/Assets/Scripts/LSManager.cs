using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSManager : MonoBehaviour
{
    // PUBLIC //

    public static LSManager instance;
    
    //reference to the player
    public LSPlayer thePlayer;



    // PRIVATE //
    private MapPoint[] allPoints;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        allPoints = FindObjectsOfType<MapPoint>();

        //find the current MapPoint
        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint mapPoint in allPoints)
            {
                if(mapPoint.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    thePlayer.transform.position = mapPoint.transform.position;
                    thePlayer.currentPoint = mapPoint;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    //coroutine for loading levels
    public IEnumerator LoadLevelCoroutine()
    {
        //play sound effect for level selected
        AudioManager.instance.PlaySFX(AudioManager.SoundEffects.LevelSelected);

        //fade to black
        LSUIController.instance.FadeToBlack();

        //wait 1sec
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeScreenSpeed) + .25f);

        //load scene from player's current point
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
