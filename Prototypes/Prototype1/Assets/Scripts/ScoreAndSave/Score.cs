using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public GameObject score, FinishLevelDisplay;
    public Spawn spawnPoint;
    public Text TimeToCompleteText;
    public int TimeToGetForFourStars = 15, TimeToGetForFiveStars = 10;
    public Sprite Star,BlankStar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //When player enter score point
        if (other.tag == "Player")
        {
            FinishLevelDisplay.SetActive(true);
            PlayerProgress.SaveProgress();

            Respawn Res = GameObject.Find("MenuLogic").GetComponent<Respawn>();

            //Display time
            score.SetActive(true);
            if(Res.lives < 3)
            {
                TimeToCompleteText.text = "YOU COMPLETED IT IN " + Mathf.RoundToInt(spawnPoint.Timer) + " SECONDS!" + "\n" + "For each live lost your maximum score is reduced";
            }
            else
            {
                TimeToCompleteText.text = "YOU COMPLETED IT IN " + Mathf.RoundToInt(spawnPoint.Timer) + " SECONDS!";
            }

            //Display stars
            int bonus = 0;
            if (spawnPoint.Timer < TimeToGetForFourStars)
            {
                bonus++;
            }
            if (spawnPoint.Timer < TimeToGetForFiveStars)
            {
                bonus++;
            }

            GameObject[] Stars = GameObject.FindGameObjectsWithTag("StarUI");
            int count = 1;
            foreach (GameObject h in Stars)
            {
                if(count <= Res.lives + bonus)
                {
                    h.GetComponent<Image>().sprite = Star;
                }
                else
                {
                    h.GetComponent<Image>().sprite = BlankStar;
                }
                count++;
            }

            //Save highscores for this level
            int oldScore = PlayerPrefs.GetInt("HighScoreLevel" + SceneManager.GetActiveScene().buildIndex);
            Debug.Log(Res.lives);
            if (Res.lives+bonus > oldScore)
            {
                PlayerPrefs.SetInt("HighScoreLevel" + SceneManager.GetActiveScene().buildIndex,(Res.lives+bonus));
            }

            GameObject.Find("MenuLogic").GetComponent<Respawn>().ResetLives();

            score.SetActive(false);
            Destroy(spawnPoint.SpawnedPlayer);

            spawnPoint.Timer = 0.00f;
        }
    }

}
