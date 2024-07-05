using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTypeCtrl : KennMonoBehaviour
{
    [SerializeField] protected List<Transform> types;
    public List<Transform> Types => types;

    protected virtual void Update()
    {
        this.CheckTypeCanShow();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTypes();
    }

    protected virtual void LoadTypes()
    {
        if (this.types.Count > 0) return;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform type = transform.GetChild(i);
            this.types.Add(type);
        }
        Debug.LogWarning(transform.name + ": LoadTypes", gameObject);
    }

    protected virtual void CheckTypeCanShow()
    {
        foreach(Transform type in types)
        {
            TypeCtrl ctrl = type.GetComponent<TypeCtrl>();
            if (ctrl.EnoughCoin) ctrl.Show();
            else ctrl.Hidden();
        }
    }
}
