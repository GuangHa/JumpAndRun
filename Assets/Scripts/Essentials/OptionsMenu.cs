using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public void SetVolume(Slider slider)
    {
        AudioListener.volume = (slider.value / 100);
    }
}
