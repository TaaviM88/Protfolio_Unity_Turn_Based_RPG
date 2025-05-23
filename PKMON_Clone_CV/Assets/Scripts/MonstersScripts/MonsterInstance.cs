using System.Collections.Generic;
using UnityEngine;

public class MonsterInstance : MonoBehaviour
{
    public Monster baseMonster;
    public int level;
    public int currentHp;
    public int experience;
    public List<Move> currentMoves;
    public IndividualStats ivs; // Individual Values (0-31 for stat variation)
    public IndividualStats evs; // Effort Values (from battles, max 510 total)
    public CalculatedStats calculatedStats; // Final stats after calculations
    public StatusCondition status; // e.g., Poisoned, Burned
}
[System.Serializable]
public struct IndividualStats
{
    public int hp, attack, defense, specialAttack, specialDefense, speed;
}

[System.Serializable]
public struct CalculatedStats
{
    public int hp, attack, defense, specialAttack, specialDefense, speed;
}
