using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
    [Header("ARRAYS MUST BE SAME SIZE")]
    public int[] BuildLimitsForLevel;
    public GameObject[] BuildingPrefabs;

    private int rotatecount = 0;
    public GameObject UIObjectToHide, binObject, ButtonPrefab, BuildingTabPanel;
    private GameObject placingObject;
    private List<Text> buttonTexts = new List<Text>();
    private bool CanBeRotated = false;

    public static bool ValidateBuildCount = true;

    private void Start()
    {
        int count = 0;
        foreach (GameObject g in BuildingPrefabs)
        {
            GameObject button = Instantiate(ButtonPrefab, transform.position, transform.rotation);
            button.transform.SetParent(BuildingTabPanel.transform);

            button.GetComponentInChildren<Text>().text = g.name + "\n" + BuildLimitsForLevel[count] + " Remaining";
            button.GetComponent<ButtonAction>().myID = count;

            buttonTexts.Add(button.GetComponentInChildren<Text>());

            count++;
        }
    }

    public void constructObject(int objectNumber)
    {
        if(BuildLimitsForLevel[objectNumber] > 0)
        {
            //Spawn prefab
            placingObject = Instantiate(BuildingPrefabs[objectNumber], transform.position, transform.rotation);
            placingObject.name = BuildingPrefabs[objectNumber].name + Random.Range(0, 1000000);
            placingObject.GetComponentInChildren<ValidateBuild>().ActivateValidator();
            //TODO DONT HAVE THIS AS THE NAME
            CanBeRotated = placingObject.GetComponent<CanBeRotated>().canBeRotated;

            UIObjectToHide.SetActive(false);
            binObject.SetActive(true);
            rotatecount = 0;
            BuildLimitsForLevel[objectNumber]--;
            buttonTexts[objectNumber].text = BuildingPrefabs[objectNumber].name + "\n" + BuildLimitsForLevel[objectNumber] + " Remaining";
        }
    }

    private void Update()
    {
        if (placingObject == null) { return; }

        if (CanBeRotated)
        {
            if (Input.GetButtonDown("Rotate"))
            {
                int[] options = new int[] { 0, 45, -45 };
                rotatecount++;
                if (rotatecount >= options.Length) { rotatecount = 0; }

                placingObject.transform.rotation = Quaternion.Euler(placingObject.transform.eulerAngles.x, placingObject.transform.eulerAngles.y, options[rotatecount]);

            }
        }

        Vector3 MousePos = GetMousePosition();

        placingObject.transform.position = MousePos;

        if(Input.GetAxisRaw("Fire1") > 0)
        {
            if (ValidateBuildCount)
            {
                placingObject.GetComponentInChildren<ValidateBuild>().DisableValidator();
                placingObject = null;
                SwitchMenu();
            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = ray.GetPoint(100);
        return new Vector3(pos.x, pos.y, 0);
    }

    public void BinIt()
    {
        Destroy(placingObject);
        SwitchMenu();
    }

    private void SwitchMenu()
    {
        binObject.SetActive(false);
        UIObjectToHide.SetActive(true);
    }

    public void UpdateBuildingIcons()
    {
        int count = 0;
        foreach(int b in BuildLimitsForLevel)
        {
            buttonTexts[count].text = BuildingPrefabs[count].name + "\n" + BuildLimitsForLevel[count] + " Remaining";
            count++;
        }

    }
}
