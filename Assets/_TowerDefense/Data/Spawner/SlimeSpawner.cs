using UnityEngine;

public class SlimeSpawner : EnemySpawner
{
    protected static SlimeSpawner instance;
    public static SlimeSpawner Instance => instance;
    protected static string slime = "Enemy_Slime";
    public static string Slime => slime;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        SlimeSpawner.instance = this;
    }
}
