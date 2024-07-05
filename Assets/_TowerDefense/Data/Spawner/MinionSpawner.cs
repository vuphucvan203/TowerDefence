using UnityEngine;

public class MinionSpawner : EnemySpawner
{
    protected static MinionSpawner instance;
    public static MinionSpawner Instance => instance;
    protected static string minion;
    public static string Minion = "Enemy_Minion";

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        MinionSpawner.instance = this;
    }
}
