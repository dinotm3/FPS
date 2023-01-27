using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;

    void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
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
