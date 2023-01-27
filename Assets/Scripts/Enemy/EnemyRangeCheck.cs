using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCheck : MonoBehaviour
{
    private Transform _transform;
    public static int _playerLayerMask = 1 << 6;
    public bool isInRange = false;
 
    public bool CheckRange(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) <= GuardBT.attackRange)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;

        }
        return isInRange;
    }
}
