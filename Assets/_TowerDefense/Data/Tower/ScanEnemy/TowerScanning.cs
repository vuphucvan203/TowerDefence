using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerScanning: KennMonoBehaviour
{
    [Header("TowerScanning")]
    [SerializeField] protected Transform enemy;
    [SerializeField] protected List<Transform> listEnemy;
    [SerializeField] protected bool hasEnemy;
    [SerializeField] protected float speed;

    protected float timer;
    protected float delay;

    protected override void Start()
    {
        base.Start();
        this.delay = 1 / this.speed;
    }

    protected virtual void Update()
    {
        this.Scan();
    }

    protected virtual void Scan()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= this.delay)
        {
            if (this.enemy == null) this.FindEnemyInRange();
            if (this.hasEnemy)
            {
                this.ActrackEnemy();
            }
            this.timer = 0f;
        }
    }

    protected virtual void ActrackEnemy()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Tower")) return;
        if (other.name.Contains("Enemy"))
        {
            this.enemy = other.transform;
            this.hasEnemy = true;
            this.listEnemy.Add(other.transform);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            this.hasEnemy = false;
            this.enemy = null;
            this.listEnemy.Remove(other.transform);
        }
    }

    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    protected virtual void FindEnemyInRange()
    {
        if (this.listEnemy.Count <= 0) return;
        this.enemy = this.listEnemy[0];
        this.hasEnemy = true;
    }
}
