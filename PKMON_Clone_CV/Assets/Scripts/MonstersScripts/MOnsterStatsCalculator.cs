using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class MonsterStatsCalculator
{
    public static void CalculateStats(MonsterInstance monster, bool assignMoves = true)
    {
        if (monster == null || monster.baseMonster == null)
        {
            Debug.LogWarning("Cannot calculate stats: Invalid monster instance or base monster.");
            return;
        }

        // Initialize IVs (random 0–31) if not set
        if (monster.ivs.hp == 0 && monster.ivs.attack == 0)
        {
            monster.ivs = new IndividualStats
            {
                hp = Random.Range(0, 32),
                attack = Random.Range(0, 32),
                defense = Random.Range(0, 32),
                specialAttack = Random.Range(0, 32),
                specialDefense = Random.Range(0, 32),
                speed = Random.Range(0, 32)
            };
        }

        // Initialize EVs (default to 0)
        if (monster.evs.hp == 0 && monster.evs.attack == 0)
        {
            monster.evs = new IndividualStats();
        }

        // Calculate stats
        monster.calculatedStats = new CalculatedStats
        {
            hp = CalculateHP(monster.baseMonster.baseStats.hp, monster.ivs.hp, monster.evs.hp, monster.level),
            attack = CalculateStat(monster.baseMonster.baseStats.attack, monster.ivs.attack, monster.evs.attack, monster.level),
            defense = CalculateStat(monster.baseMonster.baseStats.defense, monster.ivs.defense, monster.evs.defense, monster.level),
            specialAttack = CalculateStat(monster.baseMonster.baseStats.specialAttack, monster.ivs.specialAttack, monster.evs.specialAttack, monster.level),
            specialDefense = CalculateStat(monster.baseMonster.baseStats.specialDefense, monster.ivs.specialDefense, monster.evs.specialDefense, monster.level),
            speed = CalculateStat(monster.baseMonster.baseStats.speed, monster.ivs.speed, monster.evs.speed, monster.level)
        };

        // Set currentHp to max HP if not set
        if (monster.currentHp <= 0)
        {
            monster.currentHp = monster.calculatedStats.hp;
        }

        // Auto-assign moves if empty and requested
        if (assignMoves && (monster.currentMoves == null || monster.currentMoves.Count == 0))
        {
            monster.currentMoves = new List<Move>();
            if (monster.baseMonster.learnableMoves != null)
            {
                // Select up to 4 most recent moves learnable at or below current level
                var availableMoves = monster.baseMonster.learnableMoves
                    .Where(lm => lm.level <= monster.level && lm.move != null)
                    .OrderByDescending(lm => lm.level)
                    .Take(4)
                    .Select(lm => lm.move)
                    .ToList();

                foreach (var move in availableMoves)
                {
                    // Create a new move instance to avoid modifying the asset
                    Move moveInstance = Object.Instantiate(move);
                    moveInstance.pp = moveInstance.maxPP; // Reset PP
                    monster.currentMoves.Add(moveInstance);
                }

            }
          
        }
    }

    private static int CalculateHP(int baseStat, int iv, int ev, int level)
    {
        return Mathf.FloorToInt(((2 * baseStat + iv + (ev / 4)) * level / 100f)) + level + 10;
    }

    private static int CalculateStat(int baseStat, int iv, int ev, int level)
    {
        return Mathf.FloorToInt(((2 * baseStat + iv + (ev / 4)) * level / 100f) + 5);
    }
}