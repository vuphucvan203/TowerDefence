using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTower : ButtonTower
{
    [SerializeField] protected Transform tower;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTower();
    }

    protected virtual void LoadTower()
    {
        if (this.tower != null) return;
        foreach (Transform tower in this.ctrl.TowerCtrl.Towers)
        {
            if (tower.name.Contains(transform.name)) this.tower = tower;
        }
        Debug.LogWarning(transform.name + ": LoadTower", gameObject);
    }

    public override void OnClick()
    {
        TowerAbstact ctrl = this.tower.GetComponent<TowerAbstact>();
        ctrl.SetIsBuilt(true);
        CoinManager.Instance.ReductCoinAmount(ctrl.TowerSO.Levels[0].Coint);
    }

    protected override void CheckCanShow()
    {
        Transform location = this.ctrl.TowerSelected.LocationSelected;
        if(location == null) return;
        LocationCtrl locationCtrl = location.GetComponent<LocationCtrl>();
        TowerAbstact ctrl = this.tower.GetComponent<TowerAbstact>();
        int coint = CoinManager.Instance.CoinAmount;
        if (coint >= ctrl.TowerSO.Levels[0].Coint && !locationCtrl.HasTower) this.Show();
        else this.Hidden();
        
    }
}
