using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Turn
{
    [SerializeField] protected List<Enemy> enemies;
    public List<Enemy> Enemies => enemies;
}
