using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KennMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        this.LoadComponent();
    }

    protected virtual void Reset()
    {
        this.LoadComponent();
        this.SetValue();
    }

    protected virtual void LoadComponent()
    {
        //for override
    }

    protected virtual void SetValue()
    {
        //for override
    }
}
