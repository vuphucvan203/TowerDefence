using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class Spawner : KennMonoBehaviour
{
    [Header("Spawner")]
    [SerializeField] protected Transform holder;
    [SerializeField] protected Transform prefabs;
    [SerializeField] protected List<Transform> listPrefab;
    [SerializeField] protected List<Transform> poolingObj;
    public List<Transform> PrefabList => listPrefab;
    [SerializeField] protected int spawnMax;
    public int SpawnMax => spawnMax;
    public virtual void SetSpawnMax(int max) => spawnMax = max;
    [SerializeField] protected int spawnCount;
    public int SpawnCount => spawnCount;
    public virtual void SetSpawnCount(int count) => spawnCount = count;

    [SerializeField] protected bool isSpawn;

    protected override void Start()
    {
        base.Start();
        this.prefabs.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadListPrefab();
        this.LoadHolder();
    }

    protected virtual void LoadListPrefab()
    {
        this.prefabs = transform.Find("Prefabs").GetComponent<Transform>();
        if (this.listPrefab.Count > 0) return;
        for (int i = 0; i < prefabs.childCount; i++)
        {
            Transform prefab = prefabs.GetChild(i);
            if(prefab != null) this.listPrefab.Add(prefab);
        }
        Debug.LogWarning(transform.name + ": LoadListPrefab", gameObject);
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual Transform GetPrefabByName(string prefabName)
    {
        foreach(Transform prefab in this.PrefabList)
        {
            if(prefab.name == prefabName) return prefab;
        }
        return null;
    }

    public virtual void SetSpawnStatus(bool status)
    {
        this.isSpawn = status;
    }

    protected virtual bool HolderEmpty()
    {
        if (this.holder.childCount <= 0) return true;
        return false;
    }

    protected virtual Transform GetFromPool(Transform prefab)
    {
        foreach (Transform obj in this.poolingObj)
        {
            if (obj.name == prefab.name)
            {
                this.poolingObj.Remove(obj);
                return obj;
            }

        }
        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        this.poolingObj.Add(obj);
        obj.gameObject.SetActive(false);
        if(this.spawnCount <= 0) this.spawnCount = 0;
        else this.spawnCount--;
    }

    public virtual bool Ready()
    {
        return this.spawnCount == this.spawnMax;
    }
}
