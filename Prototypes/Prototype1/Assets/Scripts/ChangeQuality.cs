using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuality : MonoBehaviour
{
    public Dropdown myDropDown;
    private int currentQuality = 2;

    public void ChangeLevel()
    {
        currentQuality = myDropDown.value;
        QualitySettings.SetQualityLevel(currentQuality);
    }

}
