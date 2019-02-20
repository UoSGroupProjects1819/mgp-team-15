using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
    [Header("ARRAYS MUST BE SAME SIZE")]
    public int[] BuildLimitsForLevel;
    private int[] OriginalLimitsForLevel;

    public GameObject[] BuildingPrefabs;

    private int rotatecount = 0;
    public GameObject UIObjectToHide, binObject, ButtonPrefab, BuildingTabPanel;
    private GameObject placingObject;
    private List<Text> buttonTexts = new List<Text>();
    private bool CanBeRotated = false, placingTeleporter;
    private int BuildingCounter = 0,currentNumber;

    public static bool ValidateBuildCount = true;

    private void Start()
    {
        int count = 0;
        foreach (GameObject g in BuildingPrefabs)
        {
            if (BuildLimitsForLevel[count] == 0) { continue; }

            GameObject button = Instantiate(ButtonPrefab, transform.position, transform.rotation);
            button.transform.SetParent(BuildingTabPanel.transform);

            button.GetComponentInChildren<Text>().text = g.name + "\n" + BuildLimitsForLevel[count] + " Remaining";
            button.GetComponent<ButtonAction>().myID = count;

            buttonTexts.Add(button.GetComponentInChildren<Text>());

            count++;
        }
        OriginalLimitsForLevel = BuildLimitsForLevel;
    }

    public void constructObject(int objectNumber)
    {
        if(BuildLimitsForLevel[objectNumber] > 0)
        {
            //Spawn prefab
            placingObject = Instantiate(BuildingPrefabs[objectNumber], transform.position, transform.rotation);
            placingObject.name = BuildingPrefabs[objectNumber].name + BuildingCounter;

            if (placingObject.GetComponent<PlaceTeleporter>())
            {
                placingObject.transform.position = Vector3.zero;
                placingObject = placingObject.GetComponent<PlaceTeleporter>().A;
                placingTeleporter = true;
            }
            else
            {
                placingObject.GetComponentInChildren<ValidateBuild>().ActivateValidator();
                CanBeRotated = placingObject.GetComponent<CanBeRotated>().canBeRotated;
            }

            currentNumber = objectNumber;
            UIObjectToHide.SetActive(false);
            binObject.SetActive(true);
            rotatecount = 0;

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

        int x = Mathf.RoundToInt(MousePos.x);
        int y = Mathf.RoundToInt(MousePos.y);
        Vector3 CorrectedPosition = new Vector3(x, y,0);

        placingObject.transform.position = CorrectedPosition;

        if(Input.GetButtonDown("Fire1"))
        {
            if (placingTeleporter)
            {
                if (!placingObject.GetComponentInParent<PlaceTeleporter>().FirstPlaced)
                {
                    Debug.Log("First placed");
                    placingObject.GetComponentInParent<PlaceTeleporter>().FirstPlaced = true;
                    placingObject = placingObject.GetComponentInParent<PlaceTeleporter>().B;
                    placingObject.SetActive(true);
                }
                else
                {
                    Debug.Log("Second placed");
                    BuildLimitsForLevel[currentNumber]--;
                    placingObject = null;
                    placingTeleporter = false;
                    BuildingCounter++;
                    SwitchMenu();
                }
            }
            else if (ValidateBuildCount)
            {
                BuildLimitsForLevel[currentNumber]--;
                buttonTexts[currentNumber].text = BuildingPrefabs[currentNumber].name + "\n" + BuildLimitsForLevel[currentNumber] + " Remaining";

                placingObject.GetComponentInChildren<ValidateBuild>().DisableValidator();
                placingObject = null;
                BuildingCounter++;
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
        Destroy(placingObject.transform.parent);
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

    public void ResetLimits()
    {
        BuildLimitsForLevel = OriginalLimitsForLevel;
        UpdateBuildingIcons();
    }
}
