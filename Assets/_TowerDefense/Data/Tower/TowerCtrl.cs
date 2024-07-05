using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerCtrl : KennMonoBehaviour
{
    [SerializeField] protected TowerLocationCtrl towerLocationCtrl;
    public TowerLocationCtrl TowerLocationCtrl => towerLocationCtrl;
    //[SerializeField] protected TowerTypeCtrl towerTypeCtrl;
    //public TowerTypeCtrl TowerTypeCtrl => towerTypeCtrl;
    [SerializeField] protected List<Transform> towers;
    public List<Transform> Towers => towers;
    

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTowers();
        this.LoadTowerLocationCtrl();
        //this.LoadTowerTypeCtrl();
    }

    protected virtual void LoadTowers()
    {
        if (this.towers.Count > 0) return;
        Transform prefabs = transform.Find("Prefabs").GetComponent<Transform>();
        for(int i = 0; i < prefabs.childCount; i++)
        {
            Transform tower = prefabs.GetChild(i);
            this.towers.Add(tower);
        }
        Debug.LogWarning(transform.name + ": LoadTowers", gameObject);
    }

    protected virtual void LoadTowerLocationCtrl()
    {
        if (this.towerLocationCtrl != null) return;
        this.towerLocationCtrl = transform.Find("TowerLocation").GetComponent<TowerLocationCtrl>();
        Debug.LogWarning(transform.name + ": LoadTowerLocationCtrl", gameObject);
    }

    //protected virtual void LoadTowerTypeCtrl()
    //{
    //    if (this.towerTypeCtrl != null) return;
    //    this.towerTypeCtrl = transform.Find("TowerType").GetComponent<TowerTypeCtrl>();
    //    Debug.LogWarning(transform.name + ": LoadTowerTypeCtrl", gameObject);
    //}
}
