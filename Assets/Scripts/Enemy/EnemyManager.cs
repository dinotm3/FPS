using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : Enemy
{
    public GuardBT guardBT;
    public static int maxHP = 30;
    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        healthPoints = maxHP;
        guardBT = GetComponent<GuardBT>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public bool TakeHit(int damage)
    {
        healthPoints -= damage;
        Debug.Log("Enemy hp: " + healthPoints);
        bool isDead = healthPoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        animator.SetBool("Dead", true);
        animator.SetBool("Walking", false);
        animator.SetBool("Attacking", false);
        animator.SetBool("Idle", false);
        // Destroy(this.gameObject);
        if (GetComponent<GuardBT>() != null)
        {
            GetComponent<GuardBT>().enabled = false;

        } else
        {
            GetComponent<GuardBTNoPatrol>().enabled = false;

        }
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
