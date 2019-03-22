using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    private Spawn spawner;
    //Takes life and kills the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") { return; }

        spawner = GameObject.Find("Spawn").GetComponent<Spawn>();
        spawner.SpawnedPlayer.GetComponent<PlayerMovement>().dead = true;

        Invoke("Respawn", 0.5f);
    }

    private void Respawn()
    {
        Destroy(spawner.SpawnedPlayer);
        spawner.startPanel.SetActive(true);
        GameObject.Find("MenuLogic").GetComponent<Respawn>().TakeLive();
    }
}
