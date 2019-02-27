using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public GameObject LevelButton;
    public GameObject LevelViewScroll;

    public void LoadLevel(int number)
    {
        bool PreStarted = GameObject.Find("SaveGame").GetComponent<SaveGame>().LoadSavedLevel("/"+number);
        if (!PreStarted)
        {
            SceneManager.LoadSceneAsync(number);
        }
    }

    public void NextLevel()
    {
        int NextlevelInt = SceneManager.GetActiveScene().buildIndex + 1;

        bool PreStarted = GameObject.Find("SaveGame").GetComponent<SaveGame>().LoadSavedLevel("/" + NextlevelInt);
        if (!PreStarted)
        {
            SceneManager.LoadSceneAsync(NextlevelInt);
        }

    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0) { return; }
        for(int c = 1; c< (SceneManager.sceneCountInBuildSettings); c++)
        {
            GameObject spawn =Instantiate(LevelButton, transform.position, transform.rotation);
            spawn.GetComponentInChildren<Text>().text = "LEVEL " + c + ": " + PlayerPrefs.GetInt("HighScoreLevel" + c)+"/5 STARS";
            spawn.transform.SetParent(LevelViewScroll.transform);
            spawn.GetComponent<ButtonLoadLevel>().LevelToLoad = c;
        }
    }
}
