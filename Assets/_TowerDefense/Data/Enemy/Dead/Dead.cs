using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dead : KennMonoBehaviour
{
    [SerializeField] protected EnemyAbstact ctrl;
    [SerializeField] protected int hp;
    [SerializeField] protected bool isDead;
    public bool IsDead { get => isDead ; set => isDead = value; }
    [SerializeField] protected float timer;

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
            this.ctrl.Animator.SetBool("isDead", true);
            this.ctrl.Animator.SetBool("reset", true);
            this.timer += Time.deltaTime;
            if(this.timer > 1f)
            {
                this.ctrl.Spawner.Despawn(transform.parent);
                CoinManager.Instance.AddCoinAmount(5);
                this.timer = 0f;
            }
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
