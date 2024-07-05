using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinCtrl : EnemyAbstact
{
    protected override void SetValue()
    {
        base.SetValue();
        this._collider.radius = 0.25f;
    }
}
