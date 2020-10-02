using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    //Disables on start
    void Start()
    {
        this.gameObject.SetActive(false);
    }

}
