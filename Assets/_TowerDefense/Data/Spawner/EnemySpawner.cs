using UnityEngine;

public class EnemySpawner : Spawner
{
    public virtual Transform Spawn(string prefabName, Vector3 position, CheckPoint lane)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.Log("Don't exist " + prefabName);
            return null;
        }
        Transform newPrefab = this.GetFromPool(prefab);
        EnemyAbstact ctrl = newPrefab.GetComponent<EnemyAbstact>();
        ctrl.Moving.SetCheckPoint(lane);
        newPrefab.position = position;
        newPrefab.SetParent(this.holder);
        newPrefab.gameObject.SetActive(true);
        this.spawnCount++;
        return newPrefab;   
    }

    public override void Despawn(Transform obj)
    {
        base.Despawn(obj);
        EnemyAbstact ctrl = obj.GetComponent<EnemyAbstact>();
        ctrl.Dead.IsDead = false;
        ctrl.Dead.SetHPDefault();
        ctrl.Moving.SetCheckPoint(null);
        ctrl.Moving.ResetCheckPoint();
    }
}
