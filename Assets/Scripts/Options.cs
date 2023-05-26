using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Options : MonoBehaviour
{
    public GameObject menu;

    public void VolumeAdjuster(float value)
    {
        AudioManager.instance.ChangeVolume("Theme", value);
    }

    public void SFXAdjuster(float value)
    {
        foreach(Sound s in AudioManager.instance.sounds)
        {
            if(s.name == "Theme")
            {
                continue;
            }
            else
            {
                transform.GetChild(3).GetComponent<Slider>().value = s.source.volume;
                AudioManager.instance.ChangeVolume(s.name, value);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);

        }

        //SFX values
        Sound s = Array.Find(AudioManager.instance.sounds, sound => sound.name == "Jump");
        transform.GetChild(3).GetComponent<Slider>().value = s.source.volume;

        //Music values
        Sound m = Array.Find(AudioManager.instance.sounds, sound => sound.name == "Theme");
        transform.GetChild(2).GetComponent<Slider>().value = m.source.volume;
    }
}
