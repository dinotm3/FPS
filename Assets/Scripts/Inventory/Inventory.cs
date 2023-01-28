using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //list to store the items in the inventory
    public List<Item> items = new List<Item>();
    public int inventorySize = 20;
    private PlayerManager playerManager;
    public Pistol pistol;
    public int ammoInInventory;
    public int ammo;

    private void Awake()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        items.Add(pistol);
        ammoInInventory = 100;
        ammo = 24;
    }

    public int GetWeaponDamage()
    {
        // if pistol
        return 10;
    }
    public Pistol GetPistol()
    {
        // TODO if pistol = 10, else ..
        
        return pistol;
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
            if (playerManager.healthpoints < playerManager.maxHealth)
            {
                playerManager.healthpoints += 10;
            }
            items.Remove(itemToAdd);
            Debug.Log("Player health: " + playerManager.healthpoints);
        }

        if (itemToAdd.itemName == "Ammo")
        {
            ammoInInventory += 8;
            items.Remove(itemToAdd);
        }
    }

    //function to remove an item from the inventory
    public void RemoveItem(Item itemToRemove)
    {
        items.Remove(itemToRemove);
        Debug.Log("Removed " + itemToRemove.itemName + " from inventory");
    }
}
