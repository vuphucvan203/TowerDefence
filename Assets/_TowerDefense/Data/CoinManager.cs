using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : KennMonoBehaviour
{
    protected static CoinManager instance;
    public static CoinManager Instance => instance;
    [SerializeField] protected int coinAmount;
    public int CoinAmount { get =>  coinAmount; set => coinAmount = value; }

    protected override void Awake()
    {
        base.Awake();
        if(instance != null)
        {
            Debug.LogWarning("Only exist a " + instance.name);
            return;
        }
        CoinManager.instance = this;
    }

    public virtual void ReductCoinAmount(int amount)
    {
        this.coinAmount -= amount;
    }

    public virtual void AddCoinAmount(int amount)
    {
        this.coinAmount += amount;
    }
}
