using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAction : MonoBehaviour
{
    //Used to tell the builder which object to make
    public int myID = 0;
    public string Message = "";
    public GameObject MessageWindow;
    private GameObject spawn;

    public void Construct()
    {
        GameObject.Find("Builder").GetComponent<Builder>().constructObject(myID);
    }

    public void ShowMesssage()
    {
        if(Message == "")
        {
            return;
        }

        spawn = Instantiate(MessageWindow);
        spawn.transform.SetParent(transform.parent.transform.parent.transform);
        spawn.GetComponentInChildren<Text>().text = Message;
        spawn.transform.position = new Vector3(transform.position.x, transform.position.y-100, transform.position.z);

    }

    public void HideMessage()
    {
        if(spawn == null) { return; }
        else
        {
            Destroy(spawn);
        }
    }
}
