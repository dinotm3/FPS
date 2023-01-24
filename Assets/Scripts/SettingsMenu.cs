using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void Awake()
    {
        SetVolume(PlayerPrefs.GetFloat("volume"));
        SetVolume(PlayerPrefs.GetFloat("quality"));
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);

    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("quality", qualityIndex);
    }

    public void SetFullSCreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
