using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // PUBLIC // 

    public static PauseMenu instance;

    public string levelSelect;
    public string mainMenu;

    public GameObject pauseScreen;

    public bool isPaused;

    private void Awake()
    {
        instance = this; //set singleton instance
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pause game on ESC key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        Debug.Log("pauseunpause clicked");

        if (isPaused)
        {
            isPaused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f; //normal speed
        }
        else
        {
            isPaused = true;
            pauseScreen.SetActive(true);
            //when the game's paused, set the time scale
            Time.timeScale = 0f; //time passing in the game is zero - freeze game
        }
    }

    public void LevelSelect()
    {
        Debug.Log("level Select clicked");

        //start at the scene you were last in
        //set 'CurrentLevel' in PlayerPrefs
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(levelSelect);
        Time.timeScale = 1f; //normal speed - so it doesn't remain at 0 when the game is paused and the level changed
    }

    public void MainMenu()
    {
        Debug.Log("main menu clicked");

        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1f; //normal speed
    }
}
