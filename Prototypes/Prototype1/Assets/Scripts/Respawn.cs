using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Spawn spawnPoint;
    public static int lives = 3;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(spawnPoint.SpawnedPlayer);
        spawnPoint.startPanel.SetActive(true);
        lives = lives - 1;
    }
}
