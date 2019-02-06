using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject score;
    public Spawn spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        spawnPoint.startPanel.SetActive(true);
        score.SetActive(true);

        StartCoroutine("ResetLevel");
    }

    private IEnumerator ResetLevel()
    {
        yield return new WaitForSeconds(1);
        score.SetActive(false);
        Destroy(spawnPoint.SpawnedPlayer);
    }
}
