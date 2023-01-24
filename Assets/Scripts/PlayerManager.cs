using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private int _healthpoints;
    public Slider healthBar;
    
    private void Awake()
    {
        _healthpoints = 100;
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        Debug.Log("Player hp: " + _healthpoints);
        bool isDead = _healthpoints <= 0;
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
            healthBar.value = _healthpoints;

        }
    }
}
