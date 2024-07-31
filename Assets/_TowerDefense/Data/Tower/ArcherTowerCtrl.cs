using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerCtrl : TowerAbstact
{
    protected override void SetValue()
    {
        base.SetValue();
        this.range.radius = 6f;
    }
}
