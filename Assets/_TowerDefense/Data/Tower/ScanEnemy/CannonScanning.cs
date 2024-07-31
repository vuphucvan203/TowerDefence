using UnityEngine;

public class CannonScanning: TowerScanning
{
    [Header("CannonScanning")]
    [SerializeField] protected BombSpawner spawner;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = Transform.FindAnyObjectByType<BombSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected override void ActrackEnemy()
    {
        if (this.spawner.SpawnCount == this.spawner.SpawnMax) return;
        Vector3 rotation = new Vector3(0, 0, 0);
        this.spawner.Spawn(BombSpawner.Bomb, this.ctrl.UnitContain.position, transform, enemy, rotation, speed);
    }
}
