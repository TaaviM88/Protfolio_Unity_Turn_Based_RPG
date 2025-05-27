using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private Camera battleCamera;
    [SerializeField] private Transform playerMonsterPosition;
    [SerializeField] private Transform opponentMonsterPosition;
    [SerializeField] private BattleUI battleUI;


    private MonsterInstance playerMonster; // Current player monster
    private MonsterInstance opponentMonster; // Current opponent monster
    private List<MonsterInstance> opponentParty; // For trainer battles
    private bool isWildBattle;
    private GameObject playerMonsterObject; // Current player monster model
    private GameObject opponentMonsterObject; // Current opponent monster model

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleCamera.enabled = true;
        InitializeBattle();
    }
    private void InitializeBattle()
    {
        // Determine battle type
        isWildBattle = GameManager.Instance.wildMonster != null;
        playerMonster = GetFirstUsableMonster(GameManager.Instance.playerParty);
        opponentParty = isWildBattle ? new List<MonsterInstance> { GameManager.Instance.wildMonster }
                                    : GameManager.Instance.opponentTrainer.monsterParty;
        opponentMonster = GetFirstUsableMonster(opponentParty);

        if (playerMonster == null || opponentMonster == null)
        {
            EndBattle();
            return;
        }

        // Spawn monster models
        SpawnMonster(ref playerMonsterObject, playerMonster, playerMonsterPosition);
        SpawnMonster(ref opponentMonsterObject, opponentMonster, opponentMonsterPosition);

        // Initialize UI
        battleUI.Setup(playerMonster, opponentMonster, isWildBattle ? "Wild" : GameManager.Instance.opponentTrainer.trainerName);
    }
    private void SpawnMonster(ref GameObject monsterObject, MonsterInstance monster, Transform position)
    {
        if (monsterObject != null)
        {
            Destroy(monsterObject); // Remove previous model
        }
        if (monster.baseMonster.monsterPrefab != null)
        {
            monsterObject = Instantiate(monster.baseMonster.monsterPrefab, position);
        }
    }
    private MonsterInstance GetFirstUsableMonster(List<MonsterInstance> party)
    {
        foreach (var monster in party)
        {
            if (monster.currentHp > 0)
            {
                return monster;
            }
        }
        return null;
    }
    // Called when a monster faints
    public void OnMonsterFainted(bool isPlayerMonster)
    {
        if (isPlayerMonster)
        {
            // Destroy current player monster model
            if (playerMonsterObject != null)
            {
                Destroy(playerMonsterObject);
            }

            // Switch to next usable player monster
            playerMonster = GetFirstUsableMonster(GameManager.Instance.playerParty);
            if (playerMonster == null)
            {
                EndBattle(); // Player loses
                return;
            }
            SpawnMonster(ref playerMonsterObject, playerMonster, playerMonsterPosition);
            battleUI.UpdatePlayerMonster(playerMonster);
        }
        else
        {
            // Destroy current opponent monster model
            if (opponentMonsterObject != null)
            {
                Destroy(opponentMonsterObject);
            }

            // Switch to next usable opponent monster
            opponentMonster = GetFirstUsableMonster(opponentParty);
            if (opponentMonster == null)
            {
                EndBattle(); // Player wins
                return;
            }
            SpawnMonster(ref opponentMonsterObject, opponentMonster, opponentMonsterPosition);
            battleUI.UpdateOpponentMonster(opponentMonster);
        }
    }

    // Simple turn-based attack for testing
    public void PerformAttack(MonsterInstance attacker, MonsterInstance defender, Move move)
    {
        // Placeholder damage calculation (using type effectiveness)
        float damage = 10f * TypeEffectiveness.CalculateEffectiveness(move.element, defender.baseMonster.elements);
        defender.currentHp = Mathf.Max(0, defender.currentHp - (int)damage);
        battleUI.UpdateHP(attacker, defender);

        // Check for faints
        if (defender.currentHp <= 0)
        {
            OnMonsterFainted(defender == playerMonster);
        }
    }
    public void EndBattle()
    {
        // Trigger end battle via BattleTrigger (simulates hotkey press)
        FindAnyObjectByType<BattleTrigger>().EndBattle();
    }
}
