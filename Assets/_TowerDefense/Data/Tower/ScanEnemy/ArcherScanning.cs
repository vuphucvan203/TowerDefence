using UnityEngine;

public class ArcherScanning: TowerScanning
{
    [Header("ArcherScanning")]
    [SerializeField] protected ArrowSpawner spawner;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
        this.LoadCtrl();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = Transform.FindAnyObjectByType<ArrowSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected override void ActrackEnemy()
    {
        if (this.spawner.SpawnCount == this.spawner.SpawnMax) return;
        Vector3 angle = this.CalculateAngle();
        UnitCtrl unitCtrl = this.ctrl.UnitContain.GetComponent<UnitCtrl>();
        Vector3 position = unitCtrl.Model.position;
        this.spawner.Spawn(ArrowSpawner.Arrow, position, transform, enemy, angle, speed);
    }

    protected virtual Vector3 CalculateAngle()
    {
        Vector3 relative = transform.InverseTransformPoint(this.enemy.position);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        Vector3 rotation = new Vector3(0, 0, angle);
        return rotation;
    }
}
