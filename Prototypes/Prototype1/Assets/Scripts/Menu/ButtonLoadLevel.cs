﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLoadLevel : MonoBehaviour
{
    //Used to load specific levels
    public int LevelToLoad = 0;

    public void LoadLevel()
    {
        if(PlayerPrefs.GetInt("PlayerLevel") < LevelToLoad) { return; }
        GameObject.Find("Logic").GetComponent<LevelSelect>().LoadLevel(LevelToLoad);
    }
}
