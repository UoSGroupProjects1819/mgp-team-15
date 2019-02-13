using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private Builder buildSource;

    string path = "";
    string file = "/LevelSavaData";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

    private void Start()
    {
        path = Application.persistentDataPath + "/Data";
    }
    public void SavePlayerProgress()
    {

    }

    public void SaveLevelProgress()
    {      
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
            Debug.Log("Creating directory in: " + path);
        }
        if(File.Exists(path + file))
        {
            File.Delete(path + file);
            Debug.Log("Removing old save from: " + path + file);
        }

        StreamWriter write = File.CreateText(path + file);
        write.WriteLine(SceneManager.GetActiveScene().buildIndex);

        GameObject[] UserObjects = GameObject.FindGameObjectsWithTag("USEROBJECT");

        write.WriteLine(UserObjects.Length);

        foreach(GameObject G in UserObjects)
        {
            write.WriteLine(G.name);
            write.WriteLine(G.transform.position.x + "\n" + G.transform.position.y + "\n" + G.transform.position.z);
            write.WriteLine(G.transform.eulerAngles.x + "\n" + G.transform.eulerAngles.y + "\n" + G.transform.eulerAngles.z);
        }

        write.Close();
    }

    public void LoadSavedLevel()
    {
        StartCoroutine("LDGameFromFile");
    }

    private IEnumerator LDGameFromFile()
    {
        if (!Directory.Exists(path))
        {
            Debug.Log("No directory found to load from!");
            yield break;
        }
        if (!File.Exists(path + file))
        {
            Debug.Log("No save file found in directory!");
            yield break;
        }

        StreamReader f = new StreamReader(path + file);
        int buildIndex = int.Parse(f.ReadLine());

        AsyncOperation lod = SceneManager.LoadSceneAsync(buildIndex);
        while (!lod.isDone)
        {
            print("Loading the Scene");
            if(lod.progress >= 0.9f)
            {
                lod.allowSceneActivation = true;
            }
            yield return 1;
        }

        buildSource = GameObject.Find("Builder").GetComponent<Builder>();
        Debug.Log("Level loaded");

        int USERCOUNT = int.Parse(f.ReadLine());

        for (int a = 0; a < USERCOUNT; a++)
        {
            string ObjectName = f.ReadLine();

            float xPos = float.Parse(f.ReadLine());
            float yPos = float.Parse(f.ReadLine());
            float zPos = float.Parse(f.ReadLine());

            Vector3 CombinedPos = new Vector3(xPos, yPos, zPos);

            float xRot = float.Parse(f.ReadLine());
            float yRot = float.Parse(f.ReadLine());
            float zRot = float.Parse(f.ReadLine());

            Vector3 CombinedRot = new Vector3(xRot, yRot, zRot);

            int count = 0;
            foreach (GameObject g in buildSource.BuildingPrefabs)
            {
                if (ObjectName.Contains(g.name))
                {
                    Instantiate(g, CombinedPos, Quaternion.Euler(CombinedRot));
                    buildSource.BuildLimitsForLevel[count]--;
                    Debug.Log("Spawn object");
                }
                count++;
            }            
        }

        buildSource.UpdateBuildingIcons();

        f.Close();
        Destroy(this.gameObject);
    }
}
