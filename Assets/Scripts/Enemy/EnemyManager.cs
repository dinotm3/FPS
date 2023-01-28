using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Enemy
{
    public GuardBT guardBT;
    public static int maxHP = 100;
    private NavMeshAgent agent;

    private void Awake()
    {
        healthPoints = maxHP;
        guardBT = GetComponent<GuardBT>();
        agent = GetComponent<NavMeshAgent>();
        if (gameObject.name == "ScavengerGray")
        {
            attackRange = 20f;
        } else
        {
            attackRange = 4f;
        }
    }

    public bool TakeHit()
    {
        // play sound
        healthPoints -= 10;
        Debug.Log("Enemy hp: " + healthPoints);
        bool isDead = healthPoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Destroy(this.gameObject);
    }

    public void CounterAttack(GameObject gameObject)
    {
        //if (gameObject != null)
        //{
        //    agent.SetDestination(gameObject.transform.position);
        //    agent.stoppingDistance = 3f;

        //}
    }
}
