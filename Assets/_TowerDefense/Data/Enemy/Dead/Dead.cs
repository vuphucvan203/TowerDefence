using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dead : KennMonoBehaviour
{
    [SerializeField] protected EnemyAbstact ctrl;
    [SerializeField] protected int hp;
    [SerializeField] protected bool isDead;
    public bool IsDead { get => isDead ; set => isDead = value; }

    protected override void Start()
    {
        base.Start();
        this.SetHPDefault();
    }

    protected virtual void Update()
    {
        if (this.hp <= 0)
        {
            this.isDead = true;
            this.ctrl.Spawner.Despawn(transform.parent);
            CoinManager.Instance.CoinAmount++;
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
        this.SetHPDefault();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<EnemyAbstact>(); 
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    public virtual void SetHPDefault()
    {
        this.hp = this.ctrl.EnemySO.HP;
    }

    public virtual void Deduct(int damage) 
    {
        this.hp -= damage;
    }
}
