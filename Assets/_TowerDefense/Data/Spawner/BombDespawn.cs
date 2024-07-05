using UnityEditor.Tilemaps;
using UnityEditor.Timeline;
using UnityEngine;

public class BombDespawn : Despawn
{
    [SerializeField] protected ExplodedBombSpawner spawner;
    [SerializeField] protected bool isExploded;
    public bool IsExploded => isExploded;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = Transform.FindAnyObjectByType<ExplodedBombSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            this.isDead = true;
            this.spawner.Spawn(ExplodedBombSpawner.ExplodedBomb, transform.position);
        }
    }
}
