using UnityEngine;
using UnityEngine.UI;
using TMPro; // For TextMeshPro

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerMonsterName;
    [SerializeField] private TextMeshProUGUI opponentMonsterName;
    [SerializeField] private TextMeshProUGUI playerMonsterHP;
    [SerializeField] private TextMeshProUGUI opponentMonsterHP;
    [SerializeField] private TextMeshProUGUI battleText;
    [SerializeField] private Button[] moveButtons; // Assign 4 buttons in Inspector
    [SerializeField] private TextMeshProUGUI[] moveButtonTexts; // Text for move name/PP
    private MonsterInstance playerMonster;
    private MonsterInstance opponentMonster;
    private BattleManager battleManager;

    private readonly Color strongColor = Color.green; // RGB(0, 255, 0)
    private readonly Color weakColor = Color.red; // RGB(255, 0, 0)
    private readonly Color neutralColor = new Color(0.78f, 0.78f, 0.78f); // RGB(200, 200, 200)

    public void Setup(MonsterInstance player, MonsterInstance opponent, string battleType)
    {
        playerMonster = player;
        opponentMonster = opponent;
        battleManager = FindAnyObjectByType<BattleManager>();

        playerMonsterName.text = player.baseMonster.monsterName;
        opponentMonsterName.text = opponent.baseMonster.monsterName;
        UpdateHP();
        battleText.text = $"A {battleType} battle begins!";

        UpdateMoveButtons();
    }

    public void UpdateHP()
    {
        playerMonsterHP.text = $"HP: {playerMonster.currentHp}/{playerMonster.calculatedStats.hp}";
        opponentMonsterHP.text = $"HP: {opponentMonster.currentHp}/{opponentMonster.calculatedStats.hp}";
    }

    public void UpdateMoveButtons()
    {
        for (int i = 0; i < moveButtons.Length; i++)
        {
            if (i < playerMonster.currentMoves.Count && playerMonster.currentMoves[i] != null)
            {
                Move move = playerMonster.currentMoves[i];
                moveButtons[i].gameObject.SetActive(true);
                moveButtonTexts[i].text = $"{move.moveName} ({move.pp}/{move.maxPP} PP)";

                // Color-code based on effectiveness
                EffectivenessType type = TypeEffectiveness.GetMoveEffectivenessType(move, opponentMonster);
                Image buttonImage = moveButtons[i].GetComponent<Image>();
                buttonImage.color = type switch
                {
                    EffectivenessType.Strong => strongColor,
                    EffectivenessType.Weak => weakColor,
                    _ => neutralColor
                };

                // Assign button click
                int moveIndex = i;
                moveButtons[i].onClick.RemoveAllListeners();
                moveButtons[i].onClick.AddListener(() => battleManager.PlayerPerformMove(moveIndex));
            }
            else
            {
                moveButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateBattleText(string text)
    {
        battleText.text = text;
    }
}