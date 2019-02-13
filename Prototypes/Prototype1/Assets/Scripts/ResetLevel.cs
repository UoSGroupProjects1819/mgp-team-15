using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public void Reset()
    {
        int myNumber = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(myNumber);
    }

}
