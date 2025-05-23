using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Scriptable Objects/Monster")]
public class Monster : ScriptableObject
{
    public string monsterName;
    public int nationaldexNumber;
    public GameObject monsterPrefab;
    public List<ElementType> elements;
    public BaseStats baseStats;
    public List<LearnableMove> learnableMoves;
    public EvolutionData evolutionData;
}

[System.Serializable]
public struct BaseStats
{
    public int hp;
    public int attack;
    public int defense;
    public int specialAttack;
    public int specialDefense;
    public int speed;
}
[System.Serializable]
public struct LearnableMove
{
    public Move move; // Reference to Move ScriptableObject
    public int level; // Level at which move is learned
}

[System.Serializable]
public struct EvolutionData
{
    public Monster evolvesInto; // Reference to next evolution (e.g., Charmeleon)
    public int evolutionLevel; // Level at which evolution occurs
    //public Item requiredItem; // Optional item for evolution (e.g., Fire Stone)
}