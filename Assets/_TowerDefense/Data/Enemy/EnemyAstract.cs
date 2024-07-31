using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class EnemyAbstact : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D _collider;
    public CircleCollider2D Collider => _collider;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected EnemySpawner spawner;
    public Spawner Spawner => spawner;
    [SerializeField] protected EnemySO enemySO;
    public EnemySO EnemySO => enemySO;
    [SerializeField] protected Dead dead;
    public Dead Dead => dead;
    [SerializeField] protected MovingFollowCheckpoint moving;
    public MovingFollowCheckpoint Moving => moving;
    [SerializeField] protected Animator _animator;
    public Animator Animator => _animator;

    [SerializeField] protected SpriteRenderer sprite;
    public SpriteRenderer Sprite => sprite;
    protected bool isDamaged;
    public void SetIsDamaged(bool damaged) => isDamaged = damaged;
    protected float timer;

    protected virtual void Update()
    {
        if (this.isDamaged)
        {
            this.sprite.color = Color.red;
            this.timer += Time.deltaTime;
            if (this.timer < 0.1f) return;
            this.isDamaged = false;
            this.timer = 0f;
        }
        else this.sprite.color = Color.white;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
        this.LoadRigidbody();
        this.LoadSpawner();
        this.LoadEnemySO();
        this.LoadDead();
        this.LoadMoving();
        this.LoadModel();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<CircleCollider2D>();
        this._collider.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = transform.parent.parent.GetComponent<EnemySpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }

    protected virtual void LoadEnemySO()
    {
        if (this.enemySO != null) return;
        this.enemySO = Resources.Load<EnemySO>(transform.name);
        Debug.LogWarning(transform.name + ": LoadEnemySO", gameObject);
    }

    protected virtual void LoadDead()
    {
        if (this.dead != null) return;
        this.dead = transform.GetComponentInChildren<Dead>();
        Debug.LogWarning(transform.name + ": LoadDead", gameObject);
    }

    protected virtual void LoadMoving()
    {
        if (this.moving != null) return;
        this.moving = transform.GetComponentInChildren<MovingFollowCheckpoint>();
        Debug.LogWarning(transform.name + ": LoadMoving", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this._animator != null && this.sprite != null) return;
        this._animator = transform.Find("Model").GetComponent<Animator>();
        this.sprite = transform.Find("Model").GetComponent<SpriteRenderer>();
        this.sprite.color = Color.red;
        Debug.LogWarning(transform.name + ": LoadModel", gameObject);
    }
}
