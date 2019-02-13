using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioMixer VolumeMixer;

    public void ChangeVolume()
    {
        VolumeMixer.SetFloat("MasterVolume", VolumeSlider.value);
    }
}
