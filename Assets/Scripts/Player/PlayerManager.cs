using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    public Image gotHit;

    private void Start()
    {
        healthpoints = 100;
        maxHealth = 150;
        inventory = GetComponent<Inventory>();
        if (gotHit != null)
        {
            gotHit.gameObject.SetActive(false);
        }
    }

    IEnumerator RedScreen()
    {
        gotHit.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        gotHit.gameObject.SetActive(false);
        StopCoroutine(RedScreen());
    }

    public bool TakeHit()
    {
        StartCoroutine(RedScreen());
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
            healthBarText.text = "HEALTH: " + healthpoints.ToString() + " / " + maxHealth.ToString();
            ammoBarText.text = "AMMO: " + inventory.ammo.ToString() + " / " + inventory.ammoInInventory.ToString();
        }
    }
}
