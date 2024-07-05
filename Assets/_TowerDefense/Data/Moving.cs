using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : KennMonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected WeaponCtrl ctrl;
    protected Vector3 targetPos;
    public Vector3 TargetPos { get => targetPos; set => targetPos = value; }
    [SerializeField] protected float speed;
    [SerializeField] protected float distance;
    [SerializeField] protected float timer;

    protected virtual void Update()
    {
        this.DistanceGone();
        if (this.target == null) return;
        this.MoveToTarget();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<WeaponCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    public virtual void GetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual void MoveToTarget()
    {
        Vector3 distance = target.position - transform.position;
        TowerAbstact towerCtrl = this.ctrl.Tower.GetComponent<TowerAbstact>();
        float range = towerCtrl.Range.radius;
        transform.parent.position = Vector3.MoveTowards(transform.position, target.position, this.speed * Time.deltaTime);
        if (this.distance >= range) this.ctrl.Despawn.IsDead = true;
    }

    public virtual bool NoTarget()
    {
        if(this.target != null) return false;
        return true;
    }

    protected virtual void DistanceGone()
    {
        this.distance = Vector3.Distance(this.ctrl.Tower.position, transform.position);
    }
}
