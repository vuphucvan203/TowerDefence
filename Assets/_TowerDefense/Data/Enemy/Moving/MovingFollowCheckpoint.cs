using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFollowCheckpoint : KennMonoBehaviour
{
    [SerializeField] protected EnemyAbstact ctrl;
    [SerializeField] protected CheckPoint checkPoint;
    [SerializeField] protected float speed;
    protected int current;

    protected virtual void Update()
    {
        this.MoveFollowCheckPoint();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
        this.SetSpeedDefault();
    }

    protected virtual void LoadCtrl()
    {
        if(this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<EnemyAbstact>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void MoveFollowCheckPoint()
    {
        Transform checkPoint = this.checkPoint.ListCheckPoint[this.current];
        transform.parent.position = Vector3.MoveTowards(transform.position, checkPoint.position, Time.deltaTime * this.speed);
        if(transform.position == checkPoint.position) this.current++;
        if(current >= this.checkPoint.ListCheckPoint.Count) current = this.checkPoint.ListCheckPoint.Count - 1;
    }

    public virtual void ResetCheckPoint()
    {
        this.current = 0;
    }

    public virtual void SetCheckPoint(CheckPoint checkPoint)
    {
        this.checkPoint = checkPoint;
    }

    protected virtual void SetSpeedDefault()
    {
        this.speed = this.ctrl.EnemySO.Speed;
    }
}
