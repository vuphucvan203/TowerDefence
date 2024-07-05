using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public class LevelUpgrade
{
    [SerializeField] protected int coin;
    public int Coint => coin;
    [SerializeField] protected float speed;
    public float Speed => speed;
    [SerializeField] protected float range;
    public float Range => range;
}
