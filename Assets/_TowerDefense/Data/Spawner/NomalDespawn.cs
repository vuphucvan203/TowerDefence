using TreeEditor;
using UnityEngine;

public class NomalDespawn : Despawn
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Enemy"))
        {
            this.isDead = true;
            this.isTimeDeath = false;
            this.isHitTarget = true;
            EnemyAbstact enemyCtrl = other.GetComponentInChildren<EnemyAbstact>();
            enemyCtrl.Dead.Deduct(1);
            enemyCtrl.SetIsDamaged(true);
        }
    }
}
