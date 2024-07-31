using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildTowerCtrl : KennMonoBehaviour
{
    [SerializeField] protected TowerSelected towerSelected;
    public TowerSelected TowerSelected => towerSelected;
    [SerializeField] protected TowerCtrl towerCtrl;
    public TowerCtrl TowerCtrl => towerCtrl;
    [SerializeField] protected Transform buttons;
    

    protected override void Start()
    {
        base.Start();
        this.Hidden();
    }

    protected virtual void Update()
    {
        if (this.towerSelected.IsSelectLocation) this.Show();
        else this.Hidden();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTowerSelected();
        this.LoadTowerCtrl();
        this.LoadButtons();
    }

    protected virtual void LoadTowerSelected()
    {
        if (this.towerSelected != null) return;
        this.towerSelected = Transform.FindAnyObjectByType<TowerSelected>();
        Debug.LogWarning(transform.name + ": LoadTowerSelected", gameObject);
    }

    protected virtual void LoadTowerCtrl()
    {
        if (this.towerCtrl != null) return;
        this.towerCtrl = Transform.FindAnyObjectByType<TowerCtrl>();
        Debug.LogWarning(transform.name + ": LoadTowerCtrl", gameObject);
    }

    protected virtual void LoadButtons()
    {
        if(this.buttons != null) return;
        this.buttons = transform.Find("Buttons").GetComponent<Transform>();
        Debug.LogWarning(transform.name + ": LoadButtons", gameObject);
    }

    protected virtual void Hidden()
    {
        this.buttons.gameObject.SetActive(false);
    }

    protected virtual void Show()
    {
        LocationCtrl ctrl = this.towerSelected.LocationSelected.GetComponent<LocationCtrl>();
        transform.position = this.towerSelected.LocationSelected.position;
        this.buttons.gameObject.SetActive(true);
    }
}
