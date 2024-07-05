using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLocationCtrl : KennMonoBehaviour
{
    [SerializeField] protected List<Transform> locations;
    public List<Transform> Locations => locations;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadLocations();
    }

    protected virtual void LoadLocations()
    {
        if (this.locations.Count > 0) return;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform location = transform.GetChild(i);
            this.locations.Add(location);
        }
        Debug.LogWarning(transform.name + ": LoadLocations", gameObject);
    }
}
