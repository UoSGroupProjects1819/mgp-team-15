using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseToggle : MonoBehaviour
{
    //Toggles time between 0 and 1
    private bool paused = false;
    public Sprite PauseIcon,PlayIcon;
    public Image PauseButtonImage;

    public void TogglePause()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            PauseButtonImage.sprite = PlayIcon;
        }
        else
        {
            Time.timeScale = 1;
            PauseButtonImage.sprite = PauseIcon;
        }
    }
}