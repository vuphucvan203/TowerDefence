using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Enemy
{
    [SerializeField] protected EnemyType type;
    [SerializeField] protected int amount;
    public int Amount => amount;

    public virtual string EnemyTypeToName()
    {
        string name = "Enemy_" + this.type.ToString();
        return name;
    }
}
