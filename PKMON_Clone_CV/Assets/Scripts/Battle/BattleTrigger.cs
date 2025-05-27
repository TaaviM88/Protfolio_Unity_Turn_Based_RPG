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
        Cursor.lockState = CursorLockMode.Locked;
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
