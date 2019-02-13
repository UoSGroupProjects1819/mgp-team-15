using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevel(int number)
    {
        SceneManager.LoadSceneAsync(number);
    }

    public void NextLevel()
    {
        int NextlevelInt = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadSceneAsync(NextlevelInt);
    }
}
