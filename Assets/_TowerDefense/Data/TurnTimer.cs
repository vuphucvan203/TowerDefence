using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTimer : KennMonoBehaviour
{
    [SerializeField] TurnManager spawner;
    [SerializeField] protected float timer;
    [SerializeField] protected int turn;
    protected float delay;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.GetComponent<TurnManager>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void Update()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= 5f)
        {
            this.turn++;
            this.timer = 0f;
        }
        if (this.turn > 3) this.timer = 0f;
        //this.spawner.SetTurn(this.turn);
    }
}
