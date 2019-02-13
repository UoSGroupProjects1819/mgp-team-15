using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public GameObject score, FinishLevelDisplay;
    public Spawn spawnPoint;
    public Text TimeToCompleteText;
    public Image Star1, Star2, Star3;
    public int ThreeStarTimer, TwoStarTimer, OneStarTimer;
    public Sprite Star;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            score.SetActive(true);
            TimeToCompleteText.text = "YOU COMPLETED IT IN " + Mathf.RoundToInt(Spawn.Timer) +" SECONDS!";

            if (Spawn.Timer < ThreeStarTimer)
            {
                Star1.sprite = Star;
                Star2.sprite = Star;
                Star3.sprite = Star;
            }
            else if (Spawn.Timer < TwoStarTimer)
            {
                Star1.sprite = Star;
                Star2.sprite = Star;
            }
            else if (Spawn.Timer < ThreeStarTimer)
            {
                Star1.sprite = Star;
            }

            score.SetActive(false);
            Destroy(spawnPoint.SpawnedPlayer);
            FinishLevelDisplay.SetActive(true);

        }
    }

}
