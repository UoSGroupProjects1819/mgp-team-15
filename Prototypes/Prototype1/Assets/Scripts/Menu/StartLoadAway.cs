using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLoadAway : MonoBehaviour
{
    //Moves the loading screen away

    public GameObject LoadScreen,LoadCanvas;
    GameObject Loading;

    private void Awake()
    {
        Loading = Instantiate(LoadScreen, transform.position, transform.rotation);
        Loading.transform.SetParent(LoadCanvas.transform);
        StartCoroutine("CloseLoad");
    }

    private IEnumerator CloseLoad()
    {

        RectTransform c = Loading.GetComponent<RectTransform>();

        float height = Screen.height;
        float step = height / 16;

        float a = 0;
        do
        {
            a += step;

            c.offsetMin = new Vector2(0, a);//LowerLeftCorner
            c.offsetMax = new Vector2(0, a);//UpperRightCorner

            yield return new WaitForSeconds(0.02f);
        } while (a != height);
    }
}
