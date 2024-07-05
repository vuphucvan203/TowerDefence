using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOut : KennMonoBehaviour
{
    [SerializeField] protected bool isClick;
    public bool IsClick => isClick;
    public bool SetIsClick(bool click) => isClick = click;

    protected virtual void Update()
    {
        if (Input.GetMouseButton(1)) this.isClick = true;
    }
}
