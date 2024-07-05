using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using static UnityEngine.GraphicsBuffer;

public class WeaponSpawner : Spawner
{
    [Header("WeaponSpawner")]
    [SerializeField] protected Transform enemyTarget;
    [SerializeField] protected WeaponCtrl ctrl;

    protected virtual void Update()
    {
        if(!isSpawn) this.enemyTarget = null;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.Find("Prefabs").GetComponentInChildren<WeaponCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    public virtual Transform Spawn(string prefabName, Transform tower, Transform target, Vector3 angle)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.Log("Don't exist " + prefabName);
            return null;
        }
        Transform newPrefab = this.GetFromPool(prefab);
        newPrefab.position = tower.position;
        newPrefab.transform.eulerAngles = angle;
        newPrefab.SetParent(this.holder);
        newPrefab.gameObject.SetActive(true);
        WeaponCtrl ctrl = newPrefab.GetComponent<WeaponCtrl>();
        ctrl.Moving.GetTarget(target);
        ctrl.SetTower(tower);
        this.spawnCount++;
        return newPrefab;   
    }

    public virtual Transform Spawn(string prefabName, Vector3 position)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.Log("Don't exist " + prefabName);
            return null;
        }
        Transform newPrefab = this.GetFromPool(prefab);
        newPrefab.position = position;
        newPrefab.SetParent(this.holder);
        newPrefab.gameObject.SetActive(true);
        this.spawnCount++;
        return newPrefab;
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        WeaponCtrl ctrl = obj.GetComponent<WeaponCtrl>();
        ctrl.Despawn.IsDead = false;
    }
}
