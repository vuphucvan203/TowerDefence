using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCtrl : KennMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected TowerAbstact ctrl;

    protected override void Start()
    {
        base.Start();
        this.LoadCtrl();
    }

    protected virtual void Update()
    {
        this.ControlUnitsAction();
    }
    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadModel();
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponentInParent<TowerAbstact>();
        //Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void ControlUnitsAction()
    {
        Animator animator = model.GetComponent<Animator>();
        SpriteRenderer sprite = model.GetComponent<SpriteRenderer>();

        animator.SetBool("isUp", ctrl.Scanning.Up);
        animator.SetBool("isDown", ctrl.Scanning.Down);
        animator.SetBool("isSide", ctrl.Scanning.Left || ctrl.Scanning.Right);
        if (this.ctrl.Scanning.Right) sprite.flipX = true;
        if (this.ctrl.Scanning.Left) sprite.flipX = false;
        if (this.ctrl.Scanning.HasEnemy) animator.SetBool("isAttack", true);
        else
        {
            animator.SetBool("isAttack", false);
            animator.SetBool("isUp", false);
            animator.SetBool("isDown", false);
            animator.SetBool("isSide", false);
        }
    }
}
