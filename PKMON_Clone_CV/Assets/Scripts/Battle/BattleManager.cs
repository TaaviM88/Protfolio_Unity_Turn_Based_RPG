using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //[SerializeField] private Camera battleCamera; // Battle scene camera
    //[SerializeField] private Transform playerMonsterPosition; // Position for player's monster model
    //[SerializeField] private Transform opponentMonsterPosition; // Position for opponent's monster model
    //[SerializeField] private BattleUI battleUI; // Reference to battle UI component

    //private MonsterInstance playerMonster;
    //private MonsterInstance opponentMonster;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //// Ensure battle camera is enabled
        //battleCamera.enabled = true;

        //// Get battle data from GameManager
        //playerMonster = GameManager.Instance.playerParty[0]; // Use first monster in party
        //opponentMonster = GameManager.Instance.opponentMonster;

        //// Spawn monster models (assuming you have prefabs in Monster ScriptableObject)
        //if (playerMonster.baseMonster.modelPrefab != null)
        //{
        //    Instantiate(playerMonster.baseMonster.modelPrefab, playerMonsterPosition);
        //}
        //if (opponentMonster.baseMonster.modelPrefab != null)
        //{
        //    Instantiate(opponentMonster.baseMonster.modelPrefab, opponentMonsterPosition);
        //}

        //// Initialize UI
        //battleUI.Setup(playerMonster, opponentMonster);

        //// Start battle logic (placeholder for now)
        //Debug.Log($"Battle started: {playerMonster.baseMonster.monsterName} vs. {opponentMonster.baseMonster.monsterName}");
    }


}
