using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : WeaponSpawner
{
    protected static ArrowSpawner instance;
    public static ArrowSpawner Instance;
    protected static string arrow;
    public static string Arrow = "Arrow";

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        ArrowSpawner.instance = this;
    }
}
