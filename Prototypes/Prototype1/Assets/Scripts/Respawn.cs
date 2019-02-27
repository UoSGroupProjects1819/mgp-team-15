using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public int lives = 3;
    public Sprite EmptyHeart, FilledHeart;

    private void Start()
    {
        UpdateLives();
    }
    public void TakeLive()
    {
        lives--;
        UpdateLives();
    }
    public void UpdateLives()
    {
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
    public void ResetLives()
    {
        lives = 5;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex + "Lvl", 5);
        UpdateLives();
    }
}
