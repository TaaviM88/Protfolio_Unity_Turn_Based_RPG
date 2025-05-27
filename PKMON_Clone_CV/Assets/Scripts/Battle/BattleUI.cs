using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleUI : MonoBehaviour
{
    [SerializeField] private TMP_Text playerMonsterText;
    [SerializeField] private TMP_Text opponentMonsterText;
    [SerializeField] private TMP_Text battleInfoText; // Displays "Wild" or trainer name

    public void Setup(MonsterInstance player, MonsterInstance opponent, string opponentType)
    {
        UpdatePlayerMonster(player);
        UpdateOpponentMonster(opponent);
        battleInfoText.text = $"{opponentType} Battle";
    }
    public void UpdatePlayerMonster(MonsterInstance player)
    {
        playerMonsterText.text = $"{player.baseMonster.monsterName} (Lv {player.level}) HP: {player.currentHp}";
    }
    public void UpdateOpponentMonster(MonsterInstance opponent)
    {
        opponentMonsterText.text = $"{opponent.baseMonster.monsterName} (Lv {opponent.level}) HP: {opponent.currentHp}";
    }
    public void UpdateHP(MonsterInstance player, MonsterInstance opponent)
    {
        UpdatePlayerMonster(player);
        UpdateOpponentMonster(opponent);
    }
}
