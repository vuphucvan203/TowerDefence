using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonTower : KennMonoBehaviour
{
    [SerializeField] protected UIBuildTowerCtrl ctrl;
    [SerializeField] protected Transform model;
    [SerializeField] protected Transform clickable;

    protected virtual void Update()
    {
        this.CheckCanShow();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
        this.LoadModel();
        this.LoadClickable();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.parent.GetComponent<UIBuildTowerCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadClickable()
    {
        if (this.clickable != null) return;
        this.clickable = transform.Find("Clickable").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadClickable", gameObject);
    }

    public abstract void OnClick();

    protected abstract void CheckCanShow();

    protected virtual void Show()
    {
        this.model.gameObject.SetActive(false);
        this.clickable.gameObject.SetActive(true);
    }

    protected virtual void Hidden()
    {
        this.model.gameObject.SetActive(true);
        this.clickable?.gameObject.SetActive(false);
    }
}
