using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingFollowCheckpoint : KennMonoBehaviour
{
    [SerializeField] protected EnemyAbstact ctrl;
    [SerializeField] protected CheckPoint checkPoint;
    [SerializeField] protected float speed;
    protected int current;
    [SerializeField] protected Vector3 lastPos;
    protected bool isCheck;
    public virtual void SetCheck(bool check) => isCheck = check;
    protected Vector3 distance;

    protected override void Start()
    {
        base.Start();
        //this.lastPos = transform.parent.position;
    }

    protected virtual void Update()
    {
        this.MoveFollowCheckPoint();
        if(this.isCheck) this.CheckDirectionMove();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
        this.SetSpeedDefault();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<EnemyAbstact>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void MoveFollowCheckPoint()
    {
        Transform checkPoint = this.checkPoint.ListCheckPoint[this.current];
        transform.parent.position = Vector3.MoveTowards(transform.position, checkPoint.position, Time.deltaTime * this.speed);
        if (transform.parent.position == checkPoint.position)
        {
            this.lastPos = checkPoint.position;
            this.isCheck = true;
            this.current++;
        }
        if (current >= this.checkPoint.ListCheckPoint.Count) current = this.checkPoint.ListCheckPoint.Count - 1;
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

    protected virtual void CheckDirectionMove()
    {
        this.distance = transform.parent.position - this.lastPos;
        if (distance.y < 0)
        {
            this.ctrl.Animator.SetBool("isSide", false);
            this.ctrl.Animator.SetBool("isDown", true);
        }
        if (distance.y > 0)
        {
            this.ctrl.Animator.SetBool("isSide", false);
            this.ctrl.Animator.SetBool("isUp", true);
        }
    }
}
