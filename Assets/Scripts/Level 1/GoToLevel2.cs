using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel2 : Interact
{
    public override void Trigger()
    {
        Debug.Log("Trigger()");
        SceneManager.LoadScene("Level_2");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Level 2 triggered");

            SceneManager.LoadScene("Level_2");
        }
    }
}
