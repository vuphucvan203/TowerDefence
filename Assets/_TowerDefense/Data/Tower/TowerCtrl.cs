using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerCtrl : KennMonoBehaviour
{
    [SerializeField] protected TowerLocationCtrl towerLocationCtrl;
    public TowerLocationCtrl TowerLocationCtrl => towerLocationCtrl;
    [SerializeField] protected Transform prefabs;
    [SerializeField] protected List<Transform> towers;
    public List<Transform> Towers => towers;

    protected override void Start()
    {
        base.Start();
        this.prefabs.gameObject.SetActive(false);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTowers();
        this.LoadTowerLocationCtrl();
    }

    protected virtual void LoadTowers()
    {
        if (this.towers.Count > 0) return;
        this.prefabs = transform.Find("Prefabs").GetComponent<Transform>();
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
        this.towerLocationCtrl = transform.Find("TowerPlaces").GetComponent<TowerLocationCtrl>();
        Debug.LogWarning(transform.name + ": LoadTowerLocationCtrl", gameObject);
    }
}
