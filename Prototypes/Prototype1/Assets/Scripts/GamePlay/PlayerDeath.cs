using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    //Takes life and kills the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }
        Spawn spawner = GameObject.Find("Spawn").GetComponent<Spawn>();
        Destroy(spawner.SpawnedPlayer);
        spawner.startPanel.SetActive(true);
        GameObject.Find("MenuLogic").GetComponent<Respawn>().TakeLive();
    }
}
