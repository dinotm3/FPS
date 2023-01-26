using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int healthpoints;

    private void Awake()
    {
        healthpoints = 300;
    }

    public bool TakeHit()
    {
        healthpoints -= 10;
        Debug.Log("Enemy hp: " + healthpoints);
        bool isDead = healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Destroy(this.gameObject);
    }
}
