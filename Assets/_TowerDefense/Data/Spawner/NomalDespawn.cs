using TreeEditor;
using UnityEngine;

public class NomalDespawn : Despawn
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            this.isDead = true;
            Dead dead = other.GetComponentInChildren<Dead>();
            dead.Deduct(1);
        }
    }
}
