using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    //weapon properties
    public string weaponName;
    public int damage;
    public int ammo;
    public int maxAmmo;

    //function to fire the weapon
    public void Fire()
    {
        if (ammo <= 0)
        {
            //out of ammo
            Debug.Log("Out of ammo!");
            return;
        }
        ammo--;
        Debug.Log("Fired " + weaponName + " and did " + damage + " damage");
    }

    //function to reload the weapon
    public void Reload()
    {
        ammo = maxAmmo;
        Debug.Log("Reloaded " + weaponName);
    }
}
