using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class UpLevel : ButtonTower
{
    public override void OnClick()
    {
        this.ctrl.TowerSelected.SetUpLevel();
    }

    protected override void CheckCanShow()
    {
        Transform location = this.ctrl.TowerSelected.LocationSelected;
        if (location == null) return;
        LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
        int coin = CoinManager.Instance.CoinAmount;
        TowerAbstact tower = ctrl.Tower;
        if (tower == null || tower.UpgradeTowerLevel.MaxLevel)
        {
            this.Hidden();
            return;
        }
        if (coin >= tower.UpgradeTowerLevel.CoinNeed) this.Show();
        else this.Hidden();
    }
}
