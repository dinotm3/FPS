using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    public NavMeshAgent agent;
    private Camera camera;
    private Animator animator;
    public static int enemyLayerMask;
    private float attackRange = 2;
    private float attackCounter = 0f;
    private float attackTime = 1f;
    private EnemyRangeCheck rangeCheck;
    private PlayerManager playerManager;
    private bool isAttacking;
    private const string bearAttackAnim = "Combat Idle";
    private void Awake()
    {
        animator = GetComponent<Animator>();
        camera = Camera.main;
        enemyLayerMask = 1 << 7;
        playerManager = GetComponent<PlayerManager>();
        isAttacking = false;
        rangeCheck = GetComponent<EnemyRangeCheck>();
    }

    IEnumerator Attack(GameObject target)
    {
        if (target != null)
        {
            var enemyManager = target.GetComponent<EnemyManager>();
            if (enemyManager.healthpoints <= 0)
            {
                animator.SetBool(bearAttackAnim, false);
                animator.SetBool("WalkForward", false);
                animator.SetBool("Idle", true);
                isAttacking = false;
                StopCoroutine(Attack(target));
            }
            else
            {
                if (rangeCheck.CheckRange(enemyManager.transform.position))
                {
                    animator.SetBool(bearAttackAnim, true);
                    enemyManager.TakeHit();
                } else
                {
                    agent.SetDestination(enemyManager.transform.position);
                }
            }
            yield return new WaitForSeconds(attackTime);
            if (isAttacking)
            {
                StartCoroutine(Attack(target));
            }
        } else
        {
            animator.SetBool(bearAttackAnim, false);
            animator.SetBool("WalkForward", false);
            animator.SetBool("Idle", true);
            isAttacking = false;
            StopCoroutine(Attack(target));
        }

    }

    public void MoveOrAttack()
    {
        Ray movePosition = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(movePosition, out var hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Attacking");
                isAttacking = true;
                var target = hitInfo.collider.gameObject;
                agent.SetDestination(hitInfo.point);
                animator.SetBool("Idle", false);
                animator.SetBool("WalkForward", true);
                StartCoroutine(Attack(target));

            }
            animator.SetBool("Idle", false);
            animator.SetBool("WalkForward", true);
            agent.SetDestination(hitInfo.point);
        }

    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveOrAttack();
        }

        if (agent.remainingDistance < 1)
        {
            animator.SetBool("WalkForward", false);
            animator.SetBool("Idle", true);

            if (isAttacking)
            {
                animator.SetBool(bearAttackAnim, true);
                animator.SetBool("Idle", false);

            }
            else
            {
                animator.SetBool("WalkForward", false);
                animator.SetBool("Idle", true);

            }
        }
    }
}
