using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        this.locationMax = this.ctrl.TowerLocationCtrl.Locations.Count;
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButton(1))
        {
            this.ResetTowerLocation();
        }
        this.SelectLocation();
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
        if (this.locationSelected != null) this.isSelectLocation = true;
        foreach(Transform location in this.ctrl.TowerLocationCtrl.Locations)
        {
            LocationCtrl ctrl = location.GetComponent<LocationCtrl>();
            if (ctrl.IsClick) this.locationSelected = ctrl.transform;
        }
        
    }

    protected virtual void SelectTower()
    {
        if(this.towerSelected != null) this.isSelectTower = true;
        foreach(Transform tower in this.ctrl.Towers)
        {
            TowerAbstact ctrl = tower.GetComponent<TowerAbstact>();
            if(ctrl.IsBuilt) this.towerSelected = ctrl.transform;
        }
    }

    protected virtual void SetTowerAtLocation()
    {
        if (this.locationCount >= locationMax) return;
        LocationCtrl locationCtrl = this.locationSelected.GetComponent<LocationCtrl>();
        locationCtrl.Model.gameObject.SetActive(false);
        Transform newTower = Instantiate(this.towerSelected);
        newTower.name = this.towerSelected.name;
        LocationCtrl ctrl = this.locationSelected.GetComponent<LocationCtrl>();
        TowerAbstact towerCtrl = newTower.GetComponent<TowerAbstact>();
        newTower.position = new Vector3(locationSelected.position.x,locationSelected.position.y, 1f);
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
        foreach(Transform location in this.ctrl.TowerLocationCtrl.Locations)
        {
            LocationCtrl locationCtrl = location.GetComponent<LocationCtrl>();
            locationCtrl.SetIsClick(false);
        }
        foreach(Transform tower in this.ctrl.Towers)
        {
            TowerAbstact ctrl = tower.GetComponent <TowerAbstact>();
            ctrl.SetIsBuilt(false);
        }
    }

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
        LocationCtrl locationctrl = this.locationSelected.GetComponent<LocationCtrl>();
        TowerAbstact towerCtrl = locationctrl.Tower.GetComponent<TowerAbstact>();
        towerCtrl.Model.SetBool("reset", true);
        this.destroy = true;
    }
}


