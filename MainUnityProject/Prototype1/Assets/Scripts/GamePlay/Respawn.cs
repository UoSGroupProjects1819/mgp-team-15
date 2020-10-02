using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    //updates lives and resets them

    public int lives = 3;
    public Sprite EmptyHeart, FilledHeart;

    private void Start()
    {
        Debug.Log(lives);
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
        lives = 3;
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex + "Lvl", 3);
        UpdateLives();
    }
}
