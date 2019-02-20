using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenu : MonoBehaviour
{
    public GameObject menu;

    public void Toggle()
    {
        menu.SetActive(!menu.activeInHierarchy);
    }
}
