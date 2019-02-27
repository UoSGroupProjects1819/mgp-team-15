using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    //Used to tell the builder which object to make
    public int myID = 0;

    public void Construct()
    {
        GameObject.Find("Builder").GetComponent<Builder>().constructObject(myID);
    }
}
