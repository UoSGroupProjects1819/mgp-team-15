using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    public Image BackGroundMain, BackGroundLoading;
    public ScrollRect Scroller;

    public GameObject LevelButton;
    public GameObject LevelViewScroll;

    //Load level from build index, called from buttons
    public void LoadLevel(int number)
    {
        StartCoroutine("OpenLoad", number);
    }

    //load next level
    public void NextLevel()
    {
        int NextlevelInt = SceneManager.GetActiveScene().buildIndex + 1;

        bool PreStarted = GameObject.Find("SaveGame").GetComponent<SaveGame>().LoadSavedLevel("/" + NextlevelInt);
        if (!PreStarted)
        {
            SceneManager.LoadSceneAsync(NextlevelInt);
        }

    }

    //Spawn the buttons for each level
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0) { return; }

        for(int c = 1; c< (SceneManager.sceneCountInBuildSettings); c++)
        {
            GameObject spawn =Instantiate(LevelButton, transform.position, transform.rotation);

            //LevelLocked
            if (PlayerPrefs.GetInt("PlayerLevel") < c)
            {
                spawn.GetComponentInChildren<Text>().text = "LEVEL " + c + ": " + "LOCKED";
            }
            //LevelUnlocked
            else
            {
                spawn.GetComponentInChildren<Text>().text = "LEVEL " + c + ": " + PlayerPrefs.GetInt("HighScoreLevel" + c) + "/5 STARS";
            }

            spawn.transform.SetParent(LevelViewScroll.transform);
            spawn.GetComponent<ButtonLoadLevel>().LevelToLoad = c;
        }
    }

    //load the given level
    private IEnumerator OpenLoad(int number)
    {
        //Move the loading screen down

        RectTransform b = BackGroundMain.GetComponent<RectTransform>();
        RectTransform c = BackGroundLoading.GetComponent<RectTransform>();

        BackGroundLoading.transform.gameObject.SetActive(true);

        float height = Screen.height;
        float step = height / 16;

        float a = 0;
        do
        {
            a -= step;

            c.offsetMin = new Vector2(b.offsetMin.x, a+height);//LowerLeftCorner
            c.offsetMax = new Vector2(b.offsetMax.x, a+height);//UpperRightCorner

            yield return new WaitForSeconds(0.02f);
        } while (a != -height);

        yield return new WaitForSeconds(1f);

        //start loading the level
        bool PreStarted = GameObject.Find("SaveGame").GetComponent<SaveGame>().LoadSavedLevel("/" + number);
        if (!PreStarted)
        {
            SceneManager.LoadSceneAsync(number);
        }
    }
}
