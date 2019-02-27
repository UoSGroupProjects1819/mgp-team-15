using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public Spawn spawnPoint;
    public static int lives = 3;
    public Sprite EmptyHeart, FilledHeart; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player") { return; }

        Destroy(spawnPoint.SpawnedPlayer);
        spawnPoint.startPanel.SetActive(true);
        lives--;
        GameObject[] Hearts = GameObject.FindGameObjectsWithTag("HeartUI");
        int count = 1;
        foreach (GameObject h in Hearts)
        {
            if (count <= lives)
            {
                h.GetComponent<Image>().sprite = FilledHeart;
            }
            else
            {
                h.GetComponent<Image>().sprite = EmptyHeart;
            }
            count++;
        }
        
    }
}
