using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UpgradeTowerLevel : KennMonoBehaviour
{
    [SerializeField] protected TowerAbstact ctrl;
    [SerializeField] protected int currentLevel;
    [SerializeField] protected bool upgradeLevel;
    [SerializeField] protected int coinNeed;
    public int CoinNeed => coinNeed;
    protected int levelMax = 3;

    protected override void Start()
    {
        base.Start();
        this.currentLevel = 0;
    }

    protected virtual void Update()
    {
        if(this.currentLevel < levelMax)
        {
            this.coinNeed = this.ctrl.TowerSO.Levels[currentLevel].Coint;
        }
        if (this.currentLevel == levelMax) this.coinNeed = 0;
        if (this.upgradeLevel) this.UpgradeLevel();
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
        this.upgradeLevel = false;
        if (this.currentLevel >= levelMax)
        {
            Debug.Log("Level max");
        }
        else
        {
            int levelUp = this.currentLevel + 1;
            int coin = CoinManager.Instance.CoinAmount;
            if (coin >= this.ctrl.TowerSO.Levels[levelUp - 1].Coint) this.LevelUp(levelUp);
            else Debug.Log("Not enough coin");
        }
    }

    protected virtual void LevelUp(int levelUp)
    {
        this.currentLevel = levelUp;
        this.ctrl.SetRange(this.ctrl.TowerSO.Levels[levelUp - 1].Range);
        this.ctrl.Scanning.SetSpeed(this.ctrl.TowerSO.Levels[levelUp - 1].Speed);
        CoinManager.Instance.ReductCoinAmount(this.ctrl.TowerSO.Levels[levelUp - 1].Coint);
        Debug.Log("Level " + this.currentLevel);
    }

    public virtual void SetUpgradeLevel()
    {
        this.upgradeLevel = true;
    }
}
