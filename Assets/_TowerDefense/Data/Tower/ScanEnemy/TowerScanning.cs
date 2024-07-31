using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerScanning: KennMonoBehaviour
{
    [Header("TowerScanning")]
    [SerializeField] protected Transform enemy;
    [SerializeField] protected TowerAbstact ctrl;
    [SerializeField] protected List<Transform> listEnemy;
    [SerializeField] protected bool hasEnemy;
    public bool HasEnemy => hasEnemy;
    [SerializeField] protected float delay;
    [SerializeField] protected int speed;
    public void SetSpeed(int sp) => speed = sp;
    [SerializeField] protected bool up;
    public bool Up => up;
    [SerializeField] protected bool right;
    public bool Right => right;
    [SerializeField] protected bool down;
    public bool Down => down;
    [SerializeField] protected bool left;
    public bool Left => left;
    [SerializeField] protected float timer;

    protected virtual void Update()
    {
        this.Scan();
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = GetComponent<TowerAbstact>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void Scan()
    {
        this.timer += Time.deltaTime;
        if (this.timer >= this.delay)
        {
            if (this.enemy == null) this.FindEnemyInRange();
            if (this.hasEnemy)
            {
                this.ActrackEnemy();
            }
            this.timer = 0f;
        }
        if (this.enemy != null) this.CalculateEnemyPosition(this.enemy);
        else this.ResetAllDirection();
    }

    protected virtual void ActrackEnemy()
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Tower")) return;
        if (other.name.Contains("Enemy"))
        {
            this.enemy = other.transform;
            this.hasEnemy = true;
            this.listEnemy.Add(other.transform);
        }
        
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            this.hasEnemy = false;
            this.enemy = null;
            this.listEnemy.Remove(other.transform);
        }
    }

    public virtual void SetDelay(float delay)
    {
        this.delay = delay;
    }

    protected virtual void FindEnemyInRange()
    {
        if (this.listEnemy.Count <= 0) return;
        this.enemy = this.listEnemy[0];
        this.hasEnemy = true;
    }

    protected virtual void CalculateEnemyPosition(Transform postion)
    {
        Vector3 distance = postion.position - transform.position;
        if (distance.y > 0 && Mathf.Abs(distance.x) <= 2f)
        {
            this.ResetOtherDirection("up");
            this.up = true;
        }
        if (distance.x > 2f)
        {
            this.ResetOtherDirection("right");
            this.right = true;
        }
        if (distance.y < 0 && Mathf.Abs(distance.x) <= 2f)
        {
            this.ResetOtherDirection("down");
            this.down = true; 
        }
        if (distance.x < -2f)
        {
            this.ResetOtherDirection("left");
            this.left = true;
        }
    }

    protected virtual void ResetOtherDirection(string direction)
    {
        if (direction == "up") this.right = this.down = this.left = false;
        if (direction == "right") this.down = this.left = this.up = false;
        if (direction == "down") this.left = this.up = this.right = false;
        if (direction == "left") this.up = this.right = this.down = false;
    }

    protected virtual void ResetAllDirection()
    {
        this.up = this.right = this.down = this.left = false;
    }
}
