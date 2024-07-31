using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCtrl : EnemyAbstact
{
    protected override void SetValue()
    {
        base.SetValue();
        this._collider.radius = 0.35f;
        this._collider.offset = new Vector2(0, -0.5f);
    }
}
