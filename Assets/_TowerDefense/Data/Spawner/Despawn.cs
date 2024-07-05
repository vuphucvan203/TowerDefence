using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Despawn : KennMonoBehaviour
{
    [SerializeField] protected CircleCollider2D _collider;
    public CircleCollider2D Collider => _collider;
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected WeaponCtrl ctrl;
    [SerializeField] protected bool isDead;
    public bool IsDead { get => isDead; set => isDead = value; }
    protected float timer;

    protected virtual void Update()
    {
        if (this.isDead) this.ctrl.Spawner.Despawn(transform.parent);
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCollider();
        this.LoadRigidbody();
        this.LoadCtrl();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = transform.GetComponent<CircleCollider2D>();
        this._collider.isTrigger = true;
        this._collider.radius = 0.2f;
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadRigidbody()
    {
        if (this._rigidbody != null) return;
        this._rigidbody = transform.GetComponent<Rigidbody2D>();
        this._rigidbody.isKinematic = true;
        Debug.LogWarning(transform.name + ": LoadRigidbody", gameObject);
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<WeaponCtrl>();
        Debug.Log(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void DestroyObject()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
