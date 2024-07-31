using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCtrl : EnemyAbstact
{
    protected override void SetValue()
    {
        base.SetValue();
        this._collider.radius = 0.45f;
        this._collider.offset = new Vector2(0, -0.2f);
    }
}
