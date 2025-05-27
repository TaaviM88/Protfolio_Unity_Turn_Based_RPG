using UnityEngine;

public class NPCTrainer : MonoBehaviour
{
    [SerializeField] private string trainerName = "Trainer Bob";
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
        //dialogueText.gameObject.SetActive(false);
        //isDialogueActive = false;

        // Set trainer data in GameManager
        GameManager.Instance.opponentTrainer = trainerData;
        GameManager.Instance.wildMonster = null; // Ensure wild battle is not active

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
