using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : WeaponSpawner
{
    protected static LightningSpawner instance;
    public static LightningSpawner Instance;
    protected static string lightning = "Lightning";
    public static string Lightning => lightning;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a instance " + instance.name);
            return;
        }
        LightningSpawner.instance = this;
    }
}
