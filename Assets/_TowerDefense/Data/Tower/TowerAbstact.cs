using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class TowerAbstact : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D range;
    public CircleCollider2D Range => range;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected TowerScanning scanning;
    public TowerScanning Scanning => scanning;
    [SerializeField] protected UpgradeTowerLevel upgradeTowerLevel;
    public UpgradeTowerLevel UpgradeTowerLevel => upgradeTowerLevel;
    [SerializeField] protected TowerSO towerSO;
    public TowerSO TowerSO => towerSO;
    [SerializeField] protected bool isClick;
    public bool IsClick => isClick;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRange();
        this.LoadRigidbody();
        this.LoadScanning();
        this.LoadUpgradeTowerLevel();
        this.LoadTowerSO();
    }

    protected virtual void LoadRange()
    {
        if (this.range != null) return;
        this.range = transform.GetComponent<CircleCollider2D>();
        this.range.radius = 2.0f;
        this.range.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadRange", gameObject);
    }

    protected virtual void LoadScanning()
    {
        if (this.scanning != null) return;
        this.scanning = transform.GetComponent<TowerScanning>();
        Debug.LogWarning(transform.name + ": LoadScanning", gameObject);
    }

    protected virtual void LoadUpgradeTowerLevel()
    {
        if (this.upgradeTowerLevel != null) return;
        this.upgradeTowerLevel = transform.GetComponent<UpgradeTowerLevel>();
        Debug.LogWarning(transform.name + ": LoadUpgradeTowerLevel", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadTowerSO()
    {
        if (this.towerSO != null) return;
        this.towerSO = Resources.Load<TowerSO>(transform.name);
        Debug.LogWarning(transform.name + ": LoadTowerSO", gameObject);
    }

    public virtual void SetIsClick(bool isClick)
    {
        this.isClick = isClick;
    }

    public virtual void SetRange(float range)
    {
        this.range.radius = range;
    }
}
