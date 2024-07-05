using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : WeaponSpawner
{
    protected static BombSpawner instance;
    public static BombSpawner Instance;
    protected static string bomb = "Bomb";
    public static string Bomb => bomb;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a instance " + instance.name);
            return;
        }
        BombSpawner.instance = this;
    }
}
