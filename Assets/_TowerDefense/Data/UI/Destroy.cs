using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Destroy : ButtonTower
{

    public override void OnClick()
    {
        this.ctrl.TowerSelected.SetDestroy();
        Transform location = this.ctrl.TowerSelected.LocationSelected;
        if (location == null) return;
        LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
        CoinManager.Instance.AddCoinAmount(ctrl.Tower.TowerSO.ConditionsCoin);
    }

    protected override void CheckCanShow()
    {
        Transform location = this.ctrl.TowerSelected.LocationSelected;
        if (location == null) return;
        LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
        if (ctrl.HasTower) this.Show();
        else this.Hidden();
    }
}
