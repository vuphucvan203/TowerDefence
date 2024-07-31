using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class TowerAbstact : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D range;
    public CircleCollider2D Range => range;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Animator model;
    public Animator Model => model;
    [SerializeField] protected List<Transform> units;
    public List<Transform> Units => units;
    
    [SerializeField] protected TowerScanning scanning;
    public TowerScanning Scanning => scanning;
    [SerializeField] protected UpgradeTowerLevel upgradeTowerLevel;
    public UpgradeTowerLevel UpgradeTowerLevel => upgradeTowerLevel;
    [SerializeField] protected TowerSO towerSO;
    public TowerSO TowerSO => towerSO;
    [SerializeField] protected Transform unitContain;
    public Transform UnitContain => unitContain;
    [SerializeField] protected Transform attackRange;
    [SerializeField] protected float timer;
    [SerializeField] protected bool isBuilt;
    public bool IsBuilt => isBuilt;
    public void SetIsBuilt(bool built) => isBuilt = built;
    protected bool isNewUnit;
    protected void SetIsNewUnit(bool newUnit) => isNewUnit = newUnit;

    protected override void Start()
    {
        base.Start();
        this.isNewUnit = true;
    }
    protected virtual void Update()
    {
        this.LoadUnitContain();
        if (this.upgradeTowerLevel.Upgrade) this.isNewUnit = true;
        if (this.isNewUnit)
        {
            this.HiddenUnitContain();
            this.timer += Time.deltaTime;
            if (timer < 0.8f) return;
            this.ShowUnitContain();
            this.timer = 0;
            this.isNewUnit = false;
        }
        float scale = this.range.radius * 2;
        this.attackRange.localScale = new Vector3(scale, scale, scale);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadRange();
        this.LoadRigidbody();
        this.LoadModel();
        this.LoadUnits();
        this.LoadUnitContain();
        this.LoadScanning();
        this.LoadUpgradeTowerLevel();
        this.LoadTowerSO();
        this.LoadAttackRange();
    }

    protected virtual void LoadRange()
    {
        if (this.range != null) return;
        this.range = transform.GetComponent<CircleCollider2D>();
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

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Animator>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadUnits()
    {
        if (this.units.Count > 0) return;
        Transform unitsPrefab = transform.Find("UnitsPrefab").GetComponent<Transform>();
        unitsPrefab.gameObject.SetActive(false);
        foreach (Transform unit in unitsPrefab) this.units.Add(unit);
        Debug.LogWarning(transform.name + ": LoadUnits", gameObject);
    }

    protected virtual void LoadUnitContain()
    {
        if (this.unitContain != null) return;
        this.unitContain = transform.Find("UnitContain").GetComponent<Transform>();
        //Debug.LogWarning(transform.name + ": LoadUnitContain", gameObject);
    }

    protected virtual void LoadTowerSO()
    {
        if (this.towerSO != null) return;
        this.towerSO = Resources.Load<TowerSO>(transform.name);
        Debug.LogWarning(transform.name + ": LoadTowerSO", gameObject);
    }

    protected virtual void LoadAttackRange()
    {
        if (this.attackRange != null) return;
        this.attackRange = transform.Find("AttackRange").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadAttackRange", gameObject);
    }

    public virtual void SetRange(float range)
    {
        this.range.radius = range;
    }

    public virtual void RemoteOldUnitContain()
    {
        Destroy(this.unitContain.gameObject);
    }

    public virtual void HiddenUnitContain()
    {
        this.unitContain.gameObject.SetActive(false);
    }

    protected virtual void ShowUnitContain() 
    {
        this.unitContain.gameObject.SetActive(true);
    }

    public virtual void HiddenAttackRange()
    {
        this.attackRange.gameObject.SetActive(false);
    }

    public virtual void ShowAttackRange()
    {
        this.attackRange.gameObject.SetActive(true);
    }
}
