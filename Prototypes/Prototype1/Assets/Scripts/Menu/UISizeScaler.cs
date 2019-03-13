using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISizeScaler : MonoBehaviour
{
    void Start()
    {
        float height = Screen.height * 0.06f;
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
    }
}
