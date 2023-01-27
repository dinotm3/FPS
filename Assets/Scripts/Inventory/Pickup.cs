using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    public Inventory inventory;
    private void Awake()
    {
        inventory = GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
