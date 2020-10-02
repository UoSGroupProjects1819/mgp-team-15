using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyNDoor : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //Open Door,could have animation here
            Destroy(this.transform.parent.gameObject);
        }
    }
}
