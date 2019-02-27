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
    public int ThreeStarTimer, TwoStarTimer, OneStarTimer;
    public Sprite Star,BlankStar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FinishLevelDisplay.SetActive(true);

            Respawn Res = GameObject.Find("MenuLogic").GetComponent<Respawn>();

            score.SetActive(true);
            if(Res.lives < 3)
            {
                TimeToCompleteText.text = "YOU COMPLETED IT IN " + Mathf.RoundToInt(Spawn.Timer) + " SECONDS!" + "\n" + "For each live lost your maximum score is reduced";
            }
            else
            {
                TimeToCompleteText.text = "YOU COMPLETED IT IN " + Mathf.RoundToInt(Spawn.Timer) + " SECONDS!";
            }
            
            GameObject[] Stars = GameObject.FindGameObjectsWithTag("StarUI");
            int count = 1;
            foreach (GameObject h in Stars)
            {
                if(count <= Res.lives + 2)
                {
                    h.GetComponent<Image>().sprite = Star;
                }
                else
                {
                    h.GetComponent<Image>().sprite = BlankStar;
                }
                count++;
            }

            int oldScore = PlayerPrefs.GetInt("HighScoreLevel" + SceneManager.GetActiveScene().buildIndex);
            if (Res.lives+2 > oldScore)
            {
                PlayerPrefs.SetInt("HighScoreLevel" + SceneManager.GetActiveScene().buildIndex,Res.lives+2);
            }//THIS DOESNT WORK LIVES IS BEING SET AS 5

            GameObject.Find("MenuLogic").GetComponent<Respawn>().ResetLives();

            score.SetActive(false);
            Destroy(spawnPoint.SpawnedPlayer);

        }
    }

}
