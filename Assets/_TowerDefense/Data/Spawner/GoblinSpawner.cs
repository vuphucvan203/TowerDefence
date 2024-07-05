using UnityEngine;

public class GoblinSpawner : EnemySpawner
{
    protected static GoblinSpawner instance;
    public static GoblinSpawner Instance => instance;
    protected static string goblin;
    public static string Goblin = "Enemy_Goblin";

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        GoblinSpawner.instance = this;
    }
}
