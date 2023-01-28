using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int healthPoints;
    public int attackDamage;
    public int movementSpeed;
    public float attackRange;
}
