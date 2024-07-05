using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turn", menuName = "ScriptableObjects/TurnSO", order = 2)]
public class TurnSO : ScriptableObject
{
    [SerializeField] protected List<Turn> turns;
    public List<Turn> Turns => turns;
}
