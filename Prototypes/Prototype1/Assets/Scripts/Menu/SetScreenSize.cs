using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetScreenSize : MonoBehaviour
{
    //Keeps the cameras viewport independant from the screen resolution
    //This script is provided by "s3bu" on "https://forum.unity.com/threads/how-to-maintain-the-same-view-at-different-resolutions.542483/"

    private int current_w = 0;
    private int current_h = 0;

    private float w_amount = 33.5f*2; //Minimum units horizontally
    private float h_amount = 20f*2; //Minimum units vertically

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
        SetRes();
    }

    private void SetRes()
    {
        current_w = Screen.width;
        current_h = Screen.height;
        float width_size = (float)(w_amount * Screen.height / Screen.width * 0.5);
        float height_size = (float)(h_amount * Screen.width / Screen.height * 0.5) * ((float)Screen.height / Screen.width);
        cam.orthographicSize = Mathf.Max(height_size, width_size);
    }

    void Update()
    {
        if (Screen.width != current_w || Screen.height != current_h)
        {
            SetRes();
        }
    }
}
