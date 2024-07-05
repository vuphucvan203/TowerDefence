using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySO", order = 1)]
public class EnemySO : ScriptableObject
{
    [SerializeField] protected EnemyType enemyType;
    [SerializeField] protected int hp;
    public int HP => hp;
    [SerializeField] protected float speed;
    public float Speed => speed;
}
