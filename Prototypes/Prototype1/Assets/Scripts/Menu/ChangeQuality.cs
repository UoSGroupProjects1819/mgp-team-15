using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour
{
    public SaveGame saveScript;
    public Dropdown myDropDown;
    private int currentQuality = 2;

    //sets new quality level, found in project settings tab
    public void ChangeLevel()
    {
        PlayerPrefs.SetInt("Saved", 1);
        currentQuality = myDropDown.value;
        QualitySettings.SetQualityLevel(currentQuality);
        PlayerPrefs.SetInt("Graphics", myDropDown.value);
    }

}
