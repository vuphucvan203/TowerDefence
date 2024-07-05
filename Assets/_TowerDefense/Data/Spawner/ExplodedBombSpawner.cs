using System.Collections.Generic;
using UnityEngine;

public class ExplodedBombSpawner : WeaponSpawner
{
    protected static ExplodedBombSpawner instance;
    public static ExplodedBombSpawner Instance;
    protected static string explodedBomb = "ExplodedBomb";
    public static string ExplodedBomb => explodedBomb;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a instance " + instance.name);
            return;
        }
        ExplodedBombSpawner.instance = this;
    }
}
