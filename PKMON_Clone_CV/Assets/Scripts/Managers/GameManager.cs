using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public List<MonsterInstance> playerParty; // Player's monster party
    public MonsterInstance opponentMonster; // Wild monster or trainer's monster for battle

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //if (playerParty == null || playerParty.Count == 0)
        //{
        //    // Example: Create a dummy monster for testing
        //    playerParty = new List<MonsterInstance>
        //    {
        //        new MonsterInstance
        //        {
        //            baseMonster = ScriptableObject.CreateInstance<Monster>(),
        //            level = 5,
        //            currentHp = 20,
        //            currentMoves = new List<Move>()
        //        }
        //    };
        //    playerParty[0].baseMonster.monsterName = "TestMonster";
        //    playerParty[0].baseMonster.elements = new List<ElementType> { ElementType.Normal };
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
