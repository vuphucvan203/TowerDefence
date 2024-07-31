using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeTowerLevel : KennMonoBehaviour
{
    [SerializeField] protected TowerAbstact ctrl;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected bool upgrade;
    public bool Upgrade => upgrade;
    [SerializeField] protected int coinNeed;
    protected bool isSet;
    protected void IsSet(bool set) => isSet = set;
    public int CoinNeed => coinNeed;
    protected int levelMax = 3;
    protected bool maxLevel;
    public bool MaxLevel => maxLevel;

    protected override void Start()
    {
        base.Start();
        this.currentLevel = 1;
    }

    protected virtual void Update()
    {
        if (this.ctrl.IsBuilt)
        {
            this.ctrl.SetIsBuilt(false);
            this.BuiltTower();
        }
        if (this.currentLevel < levelMax)
        {
            this.coinNeed = this.ctrl.TowerSO.Levels[currentLevel].Coint;
        }
        if (this.currentLevel == levelMax) this.coinNeed = 0;
        if (this.currentLevel >= levelMax) this.maxLevel = true;
        if (this.upgrade) this.UpgradeLevel();

    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<TowerAbstact>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }
    
    protected virtual void UpgradeLevel()
    {
        this.upgrade = false;
        int levelUp = this.currentLevel + 1;
        int coin = CoinManager.Instance.CoinAmount;
        if (coin >= this.ctrl.TowerSO.Levels[levelUp - 1].Coint) this.LevelUp(levelUp);
        else Debug.Log("Not enough coin");
    }

    protected virtual void LevelUp(int levelUp)
    {
        this.currentLevel = levelUp;
        this.ctrl.SetRange(this.ctrl.TowerSO.Levels[levelUp - 1].Range);
        this.ctrl.Scanning.SetDelay(this.ctrl.TowerSO.Levels[levelUp - 1].Delay);
        this.ctrl.Scanning.SetSpeed(this.ctrl.TowerSO.Levels[levelUp - 1].Speed);
        this.UpTower(currentLevel);
        this.SetUnitContain(currentLevel - 1);
        
        CoinManager.Instance.ReductCoinAmount(this.ctrl.TowerSO.Levels[levelUp - 1].Coint);
        Debug.Log("Level " + this.currentLevel);
    }

    public virtual void SetUpgradeLevel()
    {
        this.upgrade = true;
    }

    protected virtual void UpTower(int levelUp)
    {
        if (levelUp == 2) this.ctrl.Model.SetBool("is2", true);
        if (levelUp == 3)
        {
            this.ctrl.Model.SetBool("is2", false);
            this.ctrl.Model.SetBool("is3", true);
        }
        //if(levelUp == 3)
        //{
        //    this.ctrl.Model.SetBool("is3", false);
        //    this.ctrl.Model.SetBool("is4", true);
        //}
    }

    public virtual void SetUnitContain(int levelUp)
    {   
        Transform prefab = null;
        this.ctrl.RemoteOldUnitContain();
        prefab = this.ctrl.Units[levelUp];
        Transform unitContain = Instantiate(prefab);
        unitContain.name = "UnitContain";
        unitContain.parent = transform;
        unitContain.position = transform.position;
    }

    protected virtual void BuiltTower()
    {
        this.SetUnitContain(currentLevel - 1);
        this.ctrl.Scanning.SetDelay(this.ctrl.TowerSO.Levels[0].Delay);
        this.ctrl.Scanning.SetSpeed(this.ctrl.TowerSO.Levels[0].Speed);
        this.ctrl.Range.radius = this.ctrl.TowerSO.Levels[0].Range;
    }
}
