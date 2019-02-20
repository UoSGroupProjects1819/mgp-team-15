using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartMiddle : MonoBehaviour
{
    // Start is called before the first frame update
    Image m_Image;
    //Set this in the Inspector
    public Sprite m_Sprite;

    void Start()
    {
        //Fetch the Image from the GameObject
        m_Image = GetComponent<Image>();
    }

    void Update()
    {

        //Press space to change the Sprite of the Image
        if (Respawn.lives == 1)
        {
            m_Image.sprite = m_Sprite;
        }
    }
}
