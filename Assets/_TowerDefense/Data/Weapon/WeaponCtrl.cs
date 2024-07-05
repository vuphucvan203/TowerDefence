using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponCtrl : KennMonoBehaviour
{
    [SerializeField] protected Moving moving;
    public Moving Moving => moving;
    [SerializeField] protected Despawn despawn;
    public Despawn Despawn => despawn;

    [SerializeField] protected Spawner spawner;
    public Spawner Spawner => spawner;
    [SerializeField] protected Transform tower;
    public Transform Tower => tower;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadMoving();
        this.LoadDespawn();
        this.LoadSpawner();
    }

    protected virtual void LoadMoving()
    {
        if (this.moving != null) return;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).name.Contains("Moving")) continue;
            else
            {
                this.moving = transform.Find("Moving").GetComponent<Moving>();
                Debug.LogWarning(transform.name + ": LoadMoving", gameObject);
            }
        }
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        this.despawn = transform.Find("Despawn").GetComponent<Despawn>();
        Debug.LogWarning(transform.name + ": LoadDespawn", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent.parent.GetComponent<Spawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    public virtual void SetTower(Transform tower)
    {
        this.tower = tower;
    }
}
