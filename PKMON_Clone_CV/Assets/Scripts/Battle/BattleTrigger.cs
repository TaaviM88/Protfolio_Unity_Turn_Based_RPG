using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private KeyCode battleHotkey = KeyCode.B; // Hotkey to toggle battle
    [SerializeField] private GameObject overworldRoot; // Root GameObject for overworld objects
    [SerializeField] private Camera overworldCamera; // Overworld camera
    private bool isInBattle = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(battleHotkey))
        {
            if (!isInBattle)
            {
                StartBattle();
            }
            else
            {
                EndBattle();
            }
        }
    }

    private void StartBattle()
    {
        // Create a dummy opponent for testing
        //GameManager.Instance.opponentMonster = new MonsterInstance
        //{
        //    baseMonster = ScriptableObject.CreateInstance<Monster>(),
        //    level = 5,
        //    currentHp = 20,
        //    currentMoves = new List<Move>()
        //};
        //GameManager.Instance.opponentMonster.baseMonster.monsterName = "WildMonster";
        //GameManager.Instance.opponentMonster.baseMonster.elements = new List<ElementType> { ElementType.Null };

        // Hide overworld elements
        //overworldRoot.SetActive(false);
        //overworldCamera.enabled = false;

        // Load battle scene additively
        SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive).completed += (op) =>
        {
            // Set battle scene as active to ensure UI and cameras work correctly
            Scene battleScene = SceneManager.GetSceneByName("BattleScene");
            SceneManager.SetActiveScene(battleScene);
        };

        isInBattle = true;
    }

    private void EndBattle()
    {
        // Unload battle scene
        SceneManager.UnloadSceneAsync("BattleScene").completed += (op) =>
        {
            // Restore overworld elements
            //overworldRoot.SetActive(true);
            //overworldCamera.enabled = true;

            // Set overworld as active scene. Change "OverworldTestScene" to your actual overworld scene name or use index
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("OverworldTestScene"));

            isInBattle = false;
        };

        // Clear battle data
        //GameManager.Instance.opponentMonster = null;
    }
}
