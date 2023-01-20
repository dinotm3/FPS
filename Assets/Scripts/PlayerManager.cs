using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _healthpoints;

    private void Awake()
    {
        _healthpoints = 20;
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
}
