﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerProgress : MonoBehaviour
{
    //Controls which levels can be played
    private void Start()
    {
        if (PlayerPrefs.GetInt("PlayerLevel") == 0)
        {
            PlayerPrefs.SetInt("PlayerLevel", 1);
        }
        Debug.Log("PlayerProgress:" + PlayerPrefs.GetInt("PlayerLevel"));
    }

    public static void SaveProgress()
    {
        if (SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetInt("PlayerLevel"))
        {
            Debug.Log("Player Level Increased to:" + (SceneManager.GetActiveScene().buildIndex + 1));
            PlayerPrefs.SetInt("PlayerLevel", (SceneManager.GetActiveScene().buildIndex+1));
        }
    }

    public void ResetPlayerLevel()
    {
        Debug.Log("Reset level");
        PlayerPrefs.SetInt("PlayerLevel", 1);
        PlayerPrefs.DeleteAll();

        var info = new DirectoryInfo(Application.persistentDataPath + "/Data");

        FileInfo[] fileInfo = info.GetFiles();

        foreach(FileInfo f in fileInfo)
        {
            f.Delete();
        }

        SceneManager.LoadSceneAsync(0);
    }
}
