using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public Sprite one, two;
    public SpriteRenderer me;
    private void Start()
    {
        InvokeRepeating("flagit",0,0.4f);
    }

    void flagit()
    {
        if(me.sprite == one)
        {
            me.sprite = two;
        }
        else
        {
            me.sprite = one;
        }
    }
}
