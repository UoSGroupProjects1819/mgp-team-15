using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Spawn spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(spawnPoint.SpawnedPlayer);
        spawnPoint.startPanel.SetActive(true);
    }
}
