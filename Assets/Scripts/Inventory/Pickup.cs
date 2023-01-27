using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Item item;
    public Inventory inventory;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inventory = other.gameObject.GetComponent<Inventory>();
            item = gameObject.GetComponent<Item>();
            Debug.Log("Item: " + item);
            Debug.Log("Inventory: " + inventory);
            inventory.AddItem(item);
            Destroy(gameObject);
        }
    }
}
