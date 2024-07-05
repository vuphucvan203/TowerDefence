using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : KennMonoBehaviour
{
    [SerializeField] protected List<Transform> listCheckPoint;
    public List<Transform> ListCheckPoint => listCheckPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadListCheckPoint();
    }

    protected virtual void LoadListCheckPoint()
    {
        if (this.listCheckPoint.Count > 0) return;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform point = transform.GetChild(i);
            listCheckPoint.Add(point);
        }
        Debug.LogWarning(transform.name + ": LoadListCheckPoint", gameObject);
    }
}
