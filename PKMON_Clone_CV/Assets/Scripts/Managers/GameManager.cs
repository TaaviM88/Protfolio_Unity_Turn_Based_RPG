using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Trainer
{
    public string trainerName; // e.g., "Trainer Bob"
    public List<MonsterInstance> monsterParty; // 1–6 monsters
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<MonsterInstance> playerParty = new List<MonsterInstance>(6); // Up to 6 monsters
    public MonsterInstance wildMonster; // For wild battles
    public Trainer opponentTrainer; // For trainer battles

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    // Clear battle-specific data
    public void ClearBattleData()
    {
        wildMonster = null;
        opponentTrainer = null;
    }
    public bool HasUsableMonsters()
    {
        foreach (var monster in playerParty)
        {
            if (monster.currentHp > 0)
            {
                return true;
            }
        }
        return false;
    }
}
