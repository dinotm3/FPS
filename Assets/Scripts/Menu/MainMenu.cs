using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public AudioManager audioManager;
    public AudioMixer audioMixer;
    private AudioSource audioSource;

    void Start()
    {
        settingsMenu.SetActive(false);
        audioSource = audioManager.GetComponent<AudioSource>();
        audioSource.Play();
    }

    public void PlayGame()
    {
        audioManager.StopSound("MainTheme");
        audioSource.Stop();
        SceneManager.LoadScene("Intro");
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);

    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
