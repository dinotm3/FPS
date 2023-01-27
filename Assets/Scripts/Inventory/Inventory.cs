using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //list to store the items in the inventory
    public List<Item> items = new List<Item>();
    public int inventorySize = 20;
    private PlayerManager playerManager;

    private void Awake()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
    }
    //function to add an item to the inventory
    public void AddItem(Item itemToAdd)
    {
        if (items.Count >= inventorySize)
        {
            //inventory is full
            Debug.Log("Inventory full!");
            return;
        }
        items.Add(itemToAdd);
        Debug.Log("Added " + itemToAdd.itemName + " to inventory");

        if (itemToAdd.itemName == "HealthItem")
        {

            items.Remove(itemToAdd);
            playerManager.healthpoints += 10;
            Debug.Log("Player health: " + playerManager.healthpoints);
        }
    }

    //function to remove an item from the inventory
    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        Debug.Log("Removed " + itemToRemove.itemName + " from inventory");
    }
}
