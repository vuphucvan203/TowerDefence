using UnityEngine;

public class OrcSpawner : EnemySpawner
{
    protected static OrcSpawner instance;
    public static OrcSpawner Instance => instance;
    protected static string orc;
    public static string Orc = "Enemy_Orc";

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        OrcSpawner.instance = this;
    }
}
