using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerSO", order = 2)]
public class TowerSO : ScriptableObject
{
    [SerializeField] protected TowerType towerType;
    [SerializeField] protected int conditionsCoin;
    public int ConditionsCoin => conditionsCoin;
    [SerializeField] protected List<LevelUpgrade> levels;
    public List<LevelUpgrade> Levels => levels;
}
