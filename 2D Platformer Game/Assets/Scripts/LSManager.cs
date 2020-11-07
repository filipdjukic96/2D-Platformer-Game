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

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    //coroutine for loading levels
    public IEnumerator LoadLevelCoroutine()
    {
        //fade to black
        LSUIController.instance.FadeToBlack();

        //wait 1sec
        yield return new WaitForSeconds((1f / LSUIController.instance.fadeScreenSpeed) + .25f);

        //load scene from player's current point
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);
    }
}
