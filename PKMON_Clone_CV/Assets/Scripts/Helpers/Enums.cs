
public enum ElementType
{
    Null,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}
public enum MoveCategory
{
    Physical, 
    Special,
    Status
}
public enum StatusCondition
{
    None,
    Poisoned,
    Burned,
    Paralyzed,
    Frozen,
    Asleep,
    Confused
}
public enum GameState
{
    MainMenu,
    InBattle,
    Exploring,
    Inventory,
    Settings,
    Menu,
    Dialog
}

public enum EffectivenessType 
{ 
    Strong,
    Weak,
    Neutral
}