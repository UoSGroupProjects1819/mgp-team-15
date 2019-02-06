using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
    [Header("ARRAYS MUST BE SAME SIZE")]
    public int[] BuildLimitsForLevel;
    public GameObject[] BuildingPrefabs;

    public GameObject UIObjectToHide, binObject, ButtonPrefab, BuildingTabPanel;
    private GameObject placingObject;
    private List<Text> buttonTexts = new List<Text>();

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

            UIObjectToHide.SetActive(false);
            binObject.SetActive(true);

            BuildLimitsForLevel[objectNumber]--;
            buttonTexts[objectNumber].text = BuildingPrefabs[objectNumber].name + "\n" + BuildLimitsForLevel[objectNumber] + " Remaining";
        }
    }

    private void Update()
    {
        if (placingObject == null) { return; }

        Vector3 MousePos = GetMousePosition();

        placingObject.transform.position = MousePos;

        if(Input.GetAxisRaw("Fire1") > 0)
        {
            placingObject = null;
            SwitchMenu();
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
}
