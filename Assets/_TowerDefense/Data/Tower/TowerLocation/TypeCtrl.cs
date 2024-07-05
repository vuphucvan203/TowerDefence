using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TypeCtrl : KennMonoBehaviour
{
    [SerializeField] protected Transform tower;
    [SerializeField] protected BoxCollider2D _collider;
    [SerializeField] protected Transform model;
    public Transform TowerType => tower;
    [SerializeField] protected bool enoughCoin;
    public bool EnoughCoin => enoughCoin;

    protected virtual void Update()
    {
        this.CheckEnoughCoin();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTower();
        this.LoadCollider();
        this.LoadModel();
    }

    protected virtual void LoadTower()
    {
        if (this.tower != null) return;
        this.tower = transform.parent.parent.Find("Prefabs").Find(transform.name).GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadTower", gameObject);
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<BoxCollider2D>();
        this._collider.size = new Vector2(0.5f, 0.5f);
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void OnMouseDown()
    {
        TowerAbstact ctrl = this.tower.GetComponent<TowerAbstact>();
        ctrl.SetIsClick(true);
    }

    protected virtual void CheckEnoughCoin()
    {
        TowerAbstact towerCtrl = this.tower.GetComponent<TowerAbstact>();
        int coin = CoinManager.Instance.CoinAmount;
        if (coin >= towerCtrl.TowerSO.ConditionsCoin) this.enoughCoin = true;
        else this.enoughCoin = false;
    }

    public virtual void Show()
    {
        this.model.gameObject.SetActive(true);
    }

    public virtual void Hidden()
    {
        this.model.gameObject.SetActive(false);
    }
}
