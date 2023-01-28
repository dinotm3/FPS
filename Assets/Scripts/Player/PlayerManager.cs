using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int healthpoints;
    public Slider healthBar;
    public Text healthBarText;
    public Text ammoBarText;
    public int maxHealth;
    public int ammo;
    public int ammoInInventory;
    public Inventory inventory;
    public PlayerController pController;

    private void Awake()
    {
        healthpoints = 100;
        maxHealth = 150;
        inventory = GetComponent<Inventory>();
  
    }

    public bool TakeHit()
    {
        healthpoints -= 10;
        Debug.Log("Player hp: " + healthpoints);
        bool isDead = healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (healthBar != null && ammoBarText != null)
        {
            healthBar.value = healthpoints;
            healthBarText.text = healthpoints.ToString() + " / " + maxHealth.ToString();
            ammoBarText.text = inventory.ammo.ToString() + " / " + inventory.ammoInInventory.ToString();
        }
    }
}
