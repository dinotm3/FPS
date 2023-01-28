using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    public Weapon pistol;
    private void Awake()
    {
        pistol.weaponName = "Pistol";
        pistol.damage = 10;
        pistol.maxAmmo = 24;
        pistol.ammo = maxAmmo;
    }
}
