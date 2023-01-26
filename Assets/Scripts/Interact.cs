using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interact : MonoBehaviour
{
    public string interactable = "Interactable";
    public abstract void Trigger();   
}
