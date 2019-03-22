using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMenu : MonoBehaviour
{
    //The small menu in the top right

    public void Exit()
    {
        SaveIt();
        Application.Quit();
    }

    public void MainMenu()
    {
        SaveIt();
        SceneManager.LoadSceneAsync(0);
    }

    private void SaveIt()
    {
        if (GameObject.Find("SaveGame"))
        {
            GameObject.Find("SaveGame").GetComponent<SaveGame>().SaveLevelProgress();
        }
    }
}
