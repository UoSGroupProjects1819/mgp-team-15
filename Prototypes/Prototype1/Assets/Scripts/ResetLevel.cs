using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetLevel : MonoBehaviour
{
    public Builder BuilderScript;
    public Spawn SpawnScript;
    public GameObject BuildMenu,EndMenu;
    public void Reset()
    {

        GameObject[] UserObjects = GameObject.FindGameObjectsWithTag("USEROBJECT");

        foreach (GameObject G in UserObjects)
        {
            Destroy(G);
        }

        BuilderScript.ResetLimits();
        Destroy(SpawnScript.SpawnedPlayer);
        BuildMenu.SetActive(true);
        EndMenu.SetActive(false);
    }

}
