using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public Builder BuilderScript;
    public Spawn SpawnScript;
    public GameObject BuildMenu,EndMenu;

    //Used by reset button
    public void Reset()
    {
        GameObject[] UserObjects = GameObject.FindGameObjectsWithTag("USEROBJECT");

        //Destroy all objects placed by user
        foreach (GameObject G in UserObjects)
        {
            Destroy(G);
        }

        //Reset amount of blocks left
        BuilderScript.ResetLimits();

        //Destroy player
        Destroy(SpawnScript.SpawnedPlayer);

        BuildMenu.SetActive(true);
        EndMenu.SetActive(false);

        Time.timeScale = 1;
    }

}
