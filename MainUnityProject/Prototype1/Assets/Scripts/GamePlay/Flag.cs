using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flag : MonoBehaviour
{
    public Sprite one, two;
    public SpriteRenderer me;
    public Image me2;

    private void Start()
    {
        InvokeRepeating("flagit",0,0.4f);
    }

    void flagit()
    {
        if (me != null)
        {
            if (me.sprite == one)
            {
                me.sprite = two;
            }
            else
            {
                me.sprite = one;
            }
        }
        else
        {
            if (me2.sprite == one)
            {
                me2.sprite = two;
            }
            else
            {
                me2.sprite = one;
            }
        }
    }
}
