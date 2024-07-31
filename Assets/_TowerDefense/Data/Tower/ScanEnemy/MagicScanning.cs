using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicScanning: TowerScanning
{
    [Header("MagicScanning")]
    [SerializeField] protected LightningSpawner spawner;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = Transform.FindAnyObjectByType<LightningSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected override void ActrackEnemy()
    {
        if (this.spawner.SpawnCount == this.spawner.SpawnMax) return;
        Vector3 rotation = new Vector3(0, 0, 0);
        this.spawner.Spawn(LightningSpawner.Lightning, this.ctrl.UnitContain.position, transform, enemy, rotation, speed);
    }
}
