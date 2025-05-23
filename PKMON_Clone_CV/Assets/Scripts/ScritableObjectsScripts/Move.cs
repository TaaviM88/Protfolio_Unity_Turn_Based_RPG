using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "Scriptable Objects/Move")]
public class Move : ScriptableObject
{
    public string moveName;
    public ElementType element;
    public int power;
    public int accuracy;
    public int pp;
    public MoveCategory category;
    public string description;
}
