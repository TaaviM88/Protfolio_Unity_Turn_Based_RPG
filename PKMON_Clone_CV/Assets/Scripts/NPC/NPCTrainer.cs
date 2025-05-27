using System.Collections.Generic;
using UnityEngine;

public class NPCTrainer : MonoBehaviour
{

    [SerializeField] private Trainer trainerData; // Assigned in Inspector
    [SerializeField] private float interactionRange = 3f; // Range for player interaction
    [TextArea(3,10)]
    [SerializeField] private string dialogueText; // UI Text for dialogue (on a Canvas)
    public Animator animator; // Animator for NPC animations
    private Transform player;
    private bool isPlayerInRange;
    private bool isDialogueActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tag player GameObject
    }

    // Update is called once per frame
    void Update()
    {
        isPlayerInRange = Vector3.Distance(transform.position, player.position) <= interactionRange;

        if (isPlayerInRange && !isDialogueActive)
        {
            //dialogueText = "Press E to talk to " + trainerName;
            //dialogueText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartBattle();
            }
        }
        //else if (!isPlayerInRange && !isDialogueActive)
        //{
        //    dialogueText.gameObject.SetActive(false);
        //}

        //// Handle dialogue progression
        //if (isDialogueActive && Input.GetKeyDown(KeyCode.E))
        //{
        //    
        //}
    }

    private void StartDialogue()
    {
        isDialogueActive = true;
        //dialogueText.text = $"{trainerName}: Want to battle? Press E to fight!";
    }
    private void StartBattle()
    {
        Trainer trainer = new Trainer
        {
            trainerName = trainerData.trainerName,
            monsterParty = new List<MonsterInstance>()
        };
        foreach (var monster in trainerData.monsterParty)
        {
            var newMonster = new MonsterInstance
            {
                baseMonster = monster.baseMonster,
                level = monster.level,
                currentHp = monster.currentHp > 0 ? monster.currentHp : monster.baseMonster.baseStats.hp,
                currentMoves = new List<Move>(monster.currentMoves),
                ivs = monster.ivs,
                evs = monster.evs,
                calculatedStats = monster.calculatedStats,
                status = monster.status
            };
            MonsterStatsCalculator.CalculateStats(newMonster, assignMoves: true);
            trainer.monsterParty.Add(newMonster);
        }
        GameManager.Instance.opponentTrainer = trainer;
        GameManager.Instance.wildMonster = null;

        // Trigger battle via BattleTrigger
        FindAnyObjectByType<BattleTrigger>().StartBattle();
    }

    // Visualize interaction range in Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
