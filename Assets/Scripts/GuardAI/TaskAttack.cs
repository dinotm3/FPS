using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private PlayerManager _playerManager;
    private float _attackTime = 1f;
    private float _attackCounter = 0f;
    private float _attackRange = 2;
    private Transform _startPosition;
    private EnemyRangeCheck _rangeCheck;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    private void Attack()
    {
        if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
            //_animator.SetBool("Attacking", true);
            _animator.SetBool("Walking", false);
            _animator.SetBool("Idle", false);


        bool enemyIsDead = _playerManager.TakeHit();
        if (enemyIsDead)
        {
        ClearData("target");
            //_animator.SetBool("Attacking", false);
        if (_animator != null && _animator.isActiveAndEnabled && _animator.gameObject.activeInHierarchy && _animator.gameObject.activeSelf)
            _animator.SetBool("Walking", true);
            _animator.SetBool("Idle", false);
        }
        else
        {
            _attackCounter = 0f;
        }
        
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target == null)
        {
            target = (Transform)GetData("target");
        }

        if (target != _lastTarget)
        {
            _playerManager = target.GetComponent<PlayerManager>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime && target != null)
        {
            _rangeCheck = _animator.GetComponentInParent<EnemyRangeCheck>();
            if (target != null)
            {
                if (_rangeCheck.CheckRange(target.position))
                {
                    Attack();
                    
                }
                _playerManager = target.GetComponent<PlayerManager>();

            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
