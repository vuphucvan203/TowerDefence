using UnityEngine;

public class BeeSpawner : EnemySpawner
{
    protected static BeeSpawner instance;
    public static BeeSpawner Instance => instance;
    protected static string bee = "Enemy_Bee";
    public static string Bee => bee;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        BeeSpawner.instance = this;
    }
}
