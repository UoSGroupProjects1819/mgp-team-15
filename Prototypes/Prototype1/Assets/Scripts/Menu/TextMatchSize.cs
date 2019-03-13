using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextMatchSize : MonoBehaviour
{
    //Sets a consistent text size to the smallest size on preffered fit

    public Text[] texts;

    void Start()
    {
        foreach (Text t in texts)
        {
            t.color = Color.clear;
        }

        Invoke("DelayUI", 0.1f);
    }

    void DelayUI()
    {
        int SmallestSize = 10000;
        foreach (Text t in texts)
        {
            if (t.cachedTextGenerator.fontSizeUsedForBestFit < SmallestSize && t.cachedTextGenerator.fontSizeUsedForBestFit != 0)
            {
                SmallestSize = t.cachedTextGenerator.fontSizeUsedForBestFit;
            }
        }
        Debug.Log(SmallestSize);
        foreach (Text t in texts)
        {
            t.resizeTextForBestFit = false;
            t.fontSize = SmallestSize;
            t.color = Color.white;
        }
    }

}
