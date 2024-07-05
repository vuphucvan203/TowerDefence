using UnityEngine;

public class ArcherScanning: TowerScanning
{
    [Header("ArcherScanning")]
    [SerializeField] protected ArrowSpawner spawner;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = Transform.FindAnyObjectByType<ArrowSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
    protected override void SetValue()
    {
        base.SetValue();
        this.speed = 2f;
    }

    protected override void ActrackEnemy()
    {
        if (this.spawner.SpawnCount == this.spawner.SpawnMax) return;
        Vector3 angle = this.CalculateAngle();
        this.spawner.Spawn(ArrowSpawner.Arrow, transform, enemy, angle);
    }

    protected virtual Vector3 CalculateAngle()
    {
        Vector3 relative = transform.InverseTransformPoint(this.enemy.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        Vector3 rotation = new Vector3(0, 0, angle);
        return rotation;
    }
}
