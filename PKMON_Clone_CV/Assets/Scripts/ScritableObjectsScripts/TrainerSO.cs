using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trainer", menuName = "Scriptable Objects/Trainer")]
public class TrainerSO : ScriptableObject
{
    [SerializeField]
    public string trainerName;
    [SerializeField]
    public List<MonsterInstance> monsterParty;
}
