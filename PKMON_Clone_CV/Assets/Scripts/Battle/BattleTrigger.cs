using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleTrigger : MonoBehaviour
{
    [SerializeField] private KeyCode battleHotkey = KeyCode.B; // Hotkey to toggle battle
    [SerializeField] private GameObject overworldRoot; // Root GameObject for overworld objects
    [SerializeField] private Camera overworldCamera; // Overworld camera
    private bool isInBattle = false;
    private bool isWildBattle = true; // Toggle between wild and trainer for testing

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(battleHotkey))
        //{
        //    if (!isInBattle)
        //    {
        //        StartBattle();
        //    }
        //    else
        //    {
        //        EndBattle();
        //    }
        //}
    }

    public void StartBattle()
    {
        if (isWildBattle)
        {
            // Create a wild monster
            GameManager.Instance.wildMonster = new MonsterInstance
            {
                baseMonster = ScriptableObject.CreateInstance<Monster>(),
                level = 5,
                currentHp = 20,
                currentMoves = new List<Move>()
            };
            GameManager.Instance.wildMonster.baseMonster.monsterName = "WildMonster";
            GameManager.Instance.wildMonster.baseMonster.elements = new List<ElementType> { ElementType.Null };
        }
        else
        {
            // Create a trainer with 2 monsters for testing
            GameManager.Instance.opponentTrainer = new Trainer
            {
                trainerName = "Trainer Bob",
                monsterParty = new List<MonsterInstance>
                {
                    new MonsterInstance
                    {
                        baseMonster = ScriptableObject.CreateInstance<Monster>(),
                        level = 6,
                        currentHp = 25,
                        currentMoves = new List<Move>()
                    },
                    new MonsterInstance
                    {
                        baseMonster = ScriptableObject.CreateInstance<Monster>(),
                        level = 7,
                        currentHp = 30,
                        currentMoves = new List<Move>()
                    }
                }
            };
            GameManager.Instance.opponentTrainer.monsterParty[0].baseMonster.monsterName = "TrainerMon1";
            GameManager.Instance.opponentTrainer.monsterParty[0].baseMonster.elements = new List<ElementType> { ElementType.Fire };
            GameManager.Instance.opponentTrainer.monsterParty[1].baseMonster.monsterName = "TrainerMon2";
            GameManager.Instance.opponentTrainer.monsterParty[1].baseMonster.elements = new List<ElementType> { ElementType.Water };
        }

        // Load battle scene additively
        TransitionManager.Instance.FadeToBattle(
        onFadeOut: () =>
        {
            // Hide overworld
                overworldRoot.SetActive(false);
                overworldCamera.enabled = false;

                // Load battle scene additively
                SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive);
        },
        onFadeIn: () =>
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("BattleScene"));
            isInBattle = true;
        }
        );

        //SceneManager.LoadSceneAsync("BattleScene", LoadSceneMode.Additive).completed += (op) =>
        //{
        //    // Set battle scene as active to ensure UI and cameras work correctly
        //    Scene battleScene = SceneManager.GetSceneByName("BattleScene");
        //    SceneManager.SetActiveScene(battleScene);
        //};

        isInBattle = true;
    }

    public void EndBattle()
    {
        TransitionManager.Instance.FadeToBattle(
            onFadeOut: () =>
            {
                // Unload battle scene
                SceneManager.UnloadSceneAsync("BattleScene");
            },
            onFadeIn: () =>
            {
                // Restore overworld
                overworldRoot.SetActive(true);
                overworldCamera.enabled = true;
                // Set overworld as active scene. Change "OverworldTestScene" to your actual overworld scene name or use index
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("OverworldTestScene"));
                isInBattle = false;
                GameManager.Instance.ClearBattleData();
            }
        );

    }
}
