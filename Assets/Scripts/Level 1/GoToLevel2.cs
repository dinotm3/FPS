using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToLevel2 : Interact
{
    void Awake()
    {
        gameObject.tag = interactable;
    }

    public override void Trigger()
    {
        SceneManager.LoadScene("Level_2");
    }
}
