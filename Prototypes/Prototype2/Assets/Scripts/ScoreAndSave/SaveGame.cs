using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour
{
    private Builder buildSource;
    public GameObject saveButton;

    string path = "";
    string file = "";

    public Dropdown GraphicsDropdown;
    public Slider VolumeSlider;

    //Keep this object in all scenes
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);    
    }

    //Set path for save files and load default settings
    private void Start()
    {
        path = Application.persistentDataPath + "/Data";
        LoadSettings();
    }

    //Save object locations, hearts
    public void SaveLevelProgress()
    {
        //Find directory and file
        file = "/" + SceneManager.GetActiveScene().buildIndex;
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

        //Create file
        StreamWriter write = File.CreateText(path + file);

        //This current scene
        write.WriteLine(SceneManager.GetActiveScene().buildIndex);

        //Save amount of lives left
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().buildIndex + "Lvl", GameObject.Find("MenuLogic").GetComponent<Respawn>().lives);

        //Find all blocks placed by player
        GameObject[] UserObjects = GameObject.FindGameObjectsWithTag("USEROBJECT");
        write.WriteLine(UserObjects.Length);

        //Save blocks type, position and rotation
        foreach(GameObject G in UserObjects)
        {
            write.WriteLine(G.name);
            write.WriteLine(G.transform.position.x + "\n" + G.transform.position.y + "\n" + G.transform.position.z);
            write.WriteLine(G.transform.eulerAngles.x + "\n" + G.transform.eulerAngles.y + "\n" + G.transform.eulerAngles.z);
        }

        write.Close();
    }

    //Check if level has been played before
    public bool LoadSavedLevel(string LevelName)
    {
        if (!Directory.Exists(path))
        {
            Debug.Log("No directory found to load from!");
            return false;
        }
        if (!File.Exists(path + LevelName))
        {
            Debug.Log("No save file found in directory!");
            return false;
        }
        StartCoroutine("LDGameFromFile", LevelName);
        return true;
    }

    //Load level from given name ( its build index)
    private IEnumerator LDGameFromFile(string LevelName)
    {
        file = LevelName;

        StreamReader f = new StreamReader(path + file);

        int buildIndex = int.Parse(f.ReadLine());

        //Switch scene
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
        
        //Get remaining lives
        GameObject.Find("MenuLogic").GetComponent<Respawn>().lives = PlayerPrefs.GetInt(SceneManager.GetActiveScene().buildIndex + "Lvl");
        GameObject.Find("MenuLogic").GetComponent<Respawn>().UpdateLives();

        int USERCOUNT = int.Parse(f.ReadLine());

        //For each player object spawn it in using data from file
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
                    GameObject spawn = Instantiate(g, CombinedPos, Quaternion.Euler(CombinedRot));
                    if (spawn.GetComponentInChildren<ValidateBuild>())
                    {
                        spawn.GetComponentInChildren<ValidateBuild>().DisableValidator();
                    }
                    buildSource.BuildLimitsForLevel[count]--;
                    Debug.Log("Spawn object");
                }
                count++;
            }            
        }

        //Change the remaining blocks for the player to reflect those they placed in the last save
        buildSource.UpdateBuildingIcons();

        f.Close();
        saveButton.SetActive(true);
    }

    //Load default settings: volume and graphics
    public void LoadSettings()
    {
        if(PlayerPrefs.GetInt("Saved") != 1) { return; }
        float Volume = PlayerPrefs.GetFloat("Volume");
        int Graphics = PlayerPrefs.GetInt("Graphics");

        VolumeSlider.value = Volume;
        GraphicsDropdown.value = Graphics;
    }
}
