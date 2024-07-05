using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HolderCtrl : MonoBehaviour
{
    [SerializeField] protected float timer;

    protected virtual void Update()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform obj = transform.GetChild(i);
            WeaponCtrl ctrl = obj.GetComponent<WeaponCtrl>();
            if (ctrl.gameObject.activeSelf) this.timer += Time.deltaTime;
            else this.timer = 0f;
            if (!ctrl.Despawn.IsDead && this.timer >= 2f)
            {
                ctrl.Despawn.IsDead = true;
                this.timer = 0;
            }
        }
    }
}
