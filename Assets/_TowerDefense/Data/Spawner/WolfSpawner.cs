using UnityEngine;

public class WolfSpawner : EnemySpawner
{
    protected static WolfSpawner instance;
    public static WolfSpawner Instance => instance;
    protected static string wolf = "Enemy_Wolf";
    public static string Wolf => wolf;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        WolfSpawner.instance = this;
    }
}
