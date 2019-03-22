using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuMusicScript : MonoBehaviour
{
    public AudioSource MenuMusic;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audios = GetComponents<AudioSource>();
        MenuMusic = audios[0];

    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
            //MenuClick.Play();
    }
}
