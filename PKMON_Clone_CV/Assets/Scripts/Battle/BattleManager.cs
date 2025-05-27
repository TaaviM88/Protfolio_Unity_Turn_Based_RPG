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
    private bool isPlayerTurn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        battleCamera.enabled = true;
        InitializeBattle();
    }
    private void InitializeBattle()
    {
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

        MonsterStatsCalculator.CalculateStats(playerMonster);
        MonsterStatsCalculator.CalculateStats(opponentMonster);


        SpawnMonster(ref playerMonsterObject, playerMonster, playerMonsterPosition);
        SpawnMonster(ref opponentMonsterObject, opponentMonster, opponentMonsterPosition);
        battleUI.Setup(playerMonster, opponentMonster, isWildBattle ? "Wild" : GameManager.Instance.opponentTrainer.trainerName);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PlayerPerformMove(int moveIndex)
    {
        if (!isPlayerTurn || moveIndex < 0 || moveIndex >= playerMonster.currentMoves.Count)
        {
            return;
        }

        Move playerMove = playerMonster.currentMoves[moveIndex];
        if (playerMove.pp <= 0)
        {
            battleUI.UpdateBattleText($"{playerMove.moveName} has no PP left!");
            return;
        }

        playerMove.pp--; // Decrease PP
        battleUI.UpdateMoveButtons(); // Update PP display
        battleUI.UpdateBattleText($"{playerMonster.baseMonster.monsterName} used {playerMove.moveName}!");

        float effectiveness = TypeEffectiveness.CalculateEffectiveness(playerMove.element, opponentMonster.baseMonster.elements);
        int damage = CalculateDamage(playerMove, playerMonster, opponentMonster, effectiveness);
        opponentMonster.currentHp = Mathf.Max(0, opponentMonster.currentHp - damage);

        battleUI.UpdateHP();
        string effectivenessText = effectiveness switch
        {
            > 1f => "It's super effective!",
            < 1f when effectiveness > 0f => "It's not very effective...",
            0f => "It had no effect!",
            _ => ""
        };
        if (!string.IsNullOrEmpty(effectivenessText))
        {
            battleUI.UpdateBattleText(effectivenessText);
        }

        if (opponentMonster.currentHp <= 0)
        {
            battleUI.UpdateBattleText($"{opponentMonster.baseMonster.monsterName} fainted!");
            if (!SwitchOpponentMonster())
            {
                EndBattle();
                return;
            }
            battleUI.UpdateMoveButtons(); // Update colors for new opponent
        }
        else
        {
            PerformOpponentMove();
        }

        isPlayerTurn = false;
        Invoke(nameof(ResetTurn), 2f);
    }
    private void PerformOpponentMove()
    {
        Move opponentMove = opponentMonster.currentMoves[Random.Range(0, opponentMonster.currentMoves.Count)];
        if (opponentMove.pp <= 0)
        {
            opponentMove = Resources.Load<Move>("Moves/Tackle"); // Fallback
        }
        else
        {
            opponentMove.pp--;
        }

        battleUI.UpdateBattleText($"{opponentMonster.baseMonster.monsterName} used {opponentMove.moveName}!");

        float effectiveness = TypeEffectiveness.CalculateEffectiveness(opponentMove.element, playerMonster.baseMonster.elements);
        int damage = CalculateDamage(opponentMove, opponentMonster, playerMonster, effectiveness);
        playerMonster.currentHp = Mathf.Max(0, playerMonster.currentHp - damage);

        battleUI.UpdateHP();
        string effectivenessText = effectiveness switch
        {
            > 1f => "It's super effective!",
            < 1f when effectiveness > 0f => "It's not very effective...",
            0f => "It had no effect!",
            _ => ""
        };
        if (!string.IsNullOrEmpty(effectivenessText))
        {
            battleUI.UpdateBattleText(effectivenessText);
        }

        if (playerMonster.currentHp <= 0)
        {
            battleUI.UpdateBattleText($"{playerMonster.baseMonster.monsterName} fainted!");
            if (!SwitchPlayerMonster())
            {
                EndBattle();
                return;
            }
            battleUI.UpdateMoveButtons();
        }
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

    private bool SwitchPlayerMonster()
    {
        playerMonster = GetFirstUsableMonster(GameManager.Instance.playerParty);
        if (playerMonster != null)
        {
            SpawnMonster(ref playerMonsterObject, playerMonster, playerMonsterPosition);
            battleUI.Setup(playerMonster, opponentMonster, isWildBattle ? "Wild" : GameManager.Instance.opponentTrainer.trainerName);
            return true;
        }
        return false;
    }

    private bool SwitchOpponentMonster()
    {
        opponentMonster = GetFirstUsableMonster(opponentParty);
        if (opponentMonster != null)
        {
            SpawnMonster(ref opponentMonsterObject, opponentMonster, opponentMonsterPosition);
            battleUI.Setup(playerMonster, opponentMonster, isWildBattle ? "Wild" : GameManager.Instance.opponentTrainer.trainerName);
            return true;
        }
        return false;
    }

    private int CalculateDamage(Move move, MonsterInstance attacker, MonsterInstance defender, float effectiveness)
    {
        int attackStat = move.category == MoveCategory.Physical ? attacker.calculatedStats.attack : attacker.calculatedStats.specialAttack;
        int defenseStat = move.category == MoveCategory.Physical ? defender.calculatedStats.defense : defender.calculatedStats.specialDefense;
        float damage = ((2 * attacker.level / 5f + 2) * move.power * attackStat / defenseStat / 50f + 2) * effectiveness;
        return Mathf.FloorToInt(damage);
    }
    private void ResetTurn()
    {
        isPlayerTurn = true;
    }
    public void EndBattle()
    {
        // Trigger end battle via BattleTrigger (simulates hotkey press)
        FindAnyObjectByType<BattleTrigger>().EndBattle();
    }
}
