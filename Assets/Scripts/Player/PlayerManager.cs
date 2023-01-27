using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int healthpoints;
    public Slider healthBar;
    private TMP_Text healthBarText;
    public int maxHealth;

    private void Awake()
    {
        healthpoints = 100;
        maxHealth = 150;
        healthBarText = GetComponent<TMP_Text>();
        Debug.Log("healthBarText: " + healthBarText);
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
        if (healthBar != null)
        {
            healthBar.value = healthpoints;
            healthBarText.text = healthpoints.ToString() + " / " + maxHealth.ToString();
        }
    }
}
