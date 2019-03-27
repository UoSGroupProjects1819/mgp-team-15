using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISizeScaler : MonoBehaviour
{
    public float PercentageOfScreenHeight = 0.06f;
    public GameObject Override;
    public GameObject childSource;
    public RectTransform[] childs;

    void Start()
    {
        float height = 0.00f;

        if (Override == null)
        {
             height = Screen.height * PercentageOfScreenHeight;
        }
        else
        {
            //height = GetComponentInParent<RectTransform>().;
        }

        childs = childSource.GetComponentsInChildren<RectTransform>();
        if (childs != null)
        {
            List<RectTransform> Fil = new List<RectTransform>();

            foreach(RectTransform f in childs)
            {
                if (f.name.Contains("Detail"))
                {
                    Fil.Add(f);
                }
            }
            foreach (RectTransform r in Fil)
            {
                r.sizeDelta = new Vector2(0, (height / (float)Fil.Count));
            }
        }
        
        GetComponent<RectTransform>().sizeDelta = new Vector2(0, height);
    }
}
