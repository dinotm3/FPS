using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public AudioManager audioManager;
    void Start()
    {
        StartCoroutine(Credits());
    }

    IEnumerator Credits()
    {
        yield return new WaitForSeconds(15);
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("Menu");
        StopCoroutine(Credits());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<AudioSource>().Stop();
            SceneManager.LoadScene("Menu");
            StopCoroutine(Credits());
        }
    }
}
