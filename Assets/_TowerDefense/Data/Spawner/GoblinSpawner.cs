using UnityEngine;

public class GoblinSpawner : EnemySpawner
{
    protected static GoblinSpawner instance;
    public static GoblinSpawner Instance => instance;
    protected static string goblin = "Enemy_Goblin";
    public static string Goblin => goblin;

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
