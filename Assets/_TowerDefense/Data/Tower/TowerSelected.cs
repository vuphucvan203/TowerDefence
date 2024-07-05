using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelected : KennMonoBehaviour
{
    [SerializeField] protected TowerCtrl ctrl;
    [SerializeField] protected Transform locationSelected;
    public Transform LocationSelected => locationSelected;
    [SerializeField] protected Transform towerSelected;
    public Transform TowerSelect => towerSelected;
    [SerializeField] protected bool destroy;
    [SerializeField] protected bool upgradeLevel;
    protected int locationCount;
    protected int locationMax;
    [SerializeField] protected bool isSelectLocation;
    public bool IsSelectLocation => isSelectLocation;
    protected bool isSelectTower;

    protected override void Start()
    {
        base.Start();
        //this.HiddenTowerType();
        this.locationMax = this.ctrl.TowerLocationCtrl.Locations.Count;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(1))
        {
            this.ResetTowerLocation();
        }
        this.SelectLocation();
        //if(this.isSelectLocation) this.CheckExistTower();
        this.SelectTower();
        if(this.isSelectTower && this.isSelectLocation) this.SetTowerAtLocation();
        if (this.destroy) this.DestroyTowerAtLocation();
        if (this.upgradeLevel) this.UpgradeLevelForTowerAtLocation();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.GetComponent<TowerCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void SelectLocation()
    {
        if(this.locationSelected != null) this.isSelectLocation = true;
        foreach(Transform location in this.ctrl.TowerLocationCtrl.Locations)
        {
            LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
            if (ctrl.IsClick) this.locationSelected = ctrl.transform;
        }
    }

    protected virtual void SelectTower()
    {
        if(this.towerSelected != null) this.isSelectTower = true;
        //foreach(Transform type in this.ctrl.TowerTypeCtrl.Types)
        //{
        //    TypeCtrl typeCtrl = type.GetComponent<TypeCtrl>();
        //    Transform tower = typeCtrl.TowerType;
        //    TowerAbstact ctrl = tower.GetComponent<TowerAbstact>();
        //    if(ctrl.IsClick) this.towerSelected = ctrl.transform;
        //}
        foreach(Transform tower in this.ctrl.Towers)
        {
            TowerAbstact ctrl = tower.GetComponent<TowerAbstact>();
            if(ctrl.IsClick) this.towerSelected = ctrl.transform;
        }
    }

    protected virtual void SetTowerAtLocation()
    {
        if (this.locationCount >= locationMax) return;
        LocationCtrl locationCtrl = this.locationSelected.GetComponent<LocationCtrl>();
        locationCtrl.Model.gameObject.SetActive(false);
        Transform newTower = Instantiate(this.towerSelected);
        newTower.name = this.towerSelected.name;
        newTower.position = new Vector3(this.locationSelected.position.x, this.locationSelected.position.y, 1f);
        newTower.SetParent(this.locationSelected);
        this.ResetTowerLocation();
        locationCount++;
    }

    protected virtual void ResetTowerLocation()
    {
        this.locationSelected = null;
        this.towerSelected = null;
        this.isSelectLocation = false;
        this.isSelectTower = false;
        //this.HiddenTowerType();
        foreach(Transform location in this.ctrl.TowerLocationCtrl.Locations)
        {
            LocationCtrl locationCtrl = location.GetComponent<LocationCtrl>();
            locationCtrl.SetIsClick(false);
        }
        foreach(Transform tower in this.ctrl.Towers)
        {
            TowerAbstact ctrl = tower.GetComponent <TowerAbstact>();
            ctrl.SetIsClick(false);
        }
    }

    //protected virtual void CheckExistTower()
    //{
    //    foreach (Transform location in this.ctrl.TowerLocationCtrl.Locations)
    //    {
    //        if (location.name == this.locationSelected.name)
    //        {
    //            LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
    //            if (ctrl.HasTower) this.HiddenTowerType();
    //            else this.ShowTowerType();
    //        }
    //    }
    //}

    //protected virtual void HiddenTowerType()
    //{
    //    this.ctrl.TowerTypeCtrl.gameObject.SetActive(false);
    //}

    //protected virtual void ShowTowerType()
    //{
    //    this.ctrl.TowerTypeCtrl.gameObject.SetActive(true);
    //}

    protected virtual void DestroyTowerAtLocation()
    {
        LocationCtrl ctrl = this.locationSelected.GetComponent<LocationCtrl>();
        ctrl.DestroyTower();
        this.destroy = false;
        this.ResetTowerLocation();
    }

    protected virtual void UpgradeLevelForTowerAtLocation()
    {
        foreach(Transform location in this.ctrl.TowerLocationCtrl.Locations)
        {
            if(location.name == this.locationSelected.name)
            {
                LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
                ctrl.Tower.UpgradeTowerLevel.SetUpgradeLevel();
                this.upgradeLevel = false;
            }
        }
        this.ResetTowerLocation();
    }

    public virtual void SetUpLevel()
    {
        this.upgradeLevel = true;
    }

    public virtual void SetDestroy()
    {
        this.destroy = true;
    }
}


