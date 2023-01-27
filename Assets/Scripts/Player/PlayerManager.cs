using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int healthpoints;
    public Slider healthBar;
    
    private void Awake()
    {
        healthpoints = 100;
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

        }
    }
}
