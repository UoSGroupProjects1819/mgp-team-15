using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menu;

    //Toggles objects active state
    public void Toggle()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}
