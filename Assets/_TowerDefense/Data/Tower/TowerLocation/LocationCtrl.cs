using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class LocationCtrl : KennMonoBehaviour
{
    [SerializeField] protected BoxCollider2D _collider;
    public BoxCollider2D Collider => _collider;
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected TowerAbstact tower;
    public TowerAbstact Tower => tower;
    [SerializeField] protected bool isClick;
    public bool IsClick => isClick;
    [SerializeField] protected bool hasTower;
    public bool HasTower => hasTower;
    [SerializeField] protected bool destroy;

    protected virtual void Update()
    {
        if (this.tower != null)
        {
            this.hasTower = true;
        }
        else this.hasTower = false;
        this.tower = transform.GetComponentInChildren<TowerAbstact>();
        if (this.destroy) this.DestroyTower();
        if (!this.hasTower) return;
        if (this.isClick) this.tower.ShowAttackRange();
        else this.tower.HiddenAttackRange();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
        this.LoadModel();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<BoxCollider2D>();
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponentInChildren<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void OnMouseDown()
    {
        this.isClick = true;
    }

    public virtual void SetIsClick(bool isClick)
    { 
        this.isClick = isClick; 
    }

    public virtual void DestroyTower()
    {
        Destroy(this.tower.gameObject);
        this.destroy = false;
        this.model.gameObject.SetActive(true);
    }
}
