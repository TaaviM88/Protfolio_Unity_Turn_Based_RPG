using System.Collections.Generic;
using UnityEngine;

public static class TypeEffectiveness
{
    public static Dictionary<(ElementType attacker, ElementType defender), float> chart = new()
    {
        //Normal
        { (ElementType.Normal, ElementType.Normal), 1f },
        { (ElementType.Normal, ElementType.Fire), 1f },
        { (ElementType.Normal, ElementType.Water), 1f },
        { (ElementType.Normal, ElementType.Electric), 1f },
        { (ElementType.Normal, ElementType.Grass), 1f },
        { (ElementType.Normal, ElementType.Ice), 1f },
        { (ElementType.Normal, ElementType.Fighting), 1f },
        { (ElementType.Normal, ElementType.Poison), 1f },
        { (ElementType.Normal, ElementType.Ground), 1f },
        { (ElementType.Normal, ElementType.Flying), 1f },
        { (ElementType.Normal, ElementType.Psychic), 1f },
        { (ElementType.Normal, ElementType.Bug), 1f },
        { (ElementType.Normal, ElementType.Rock), 0.5f },
        { (ElementType.Normal, ElementType.Ghost), 0f }, // Normal is ineffective against Ghost
        { (ElementType.Normal, ElementType.Dragon), 1f },
        //Fire
        { (ElementType.Fire, ElementType.Normal), 1f },
        { (ElementType.Fire, ElementType.Fire), 0.5f }, // Fire resists Fire
        { (ElementType.Fire, ElementType.Water), 0.5f },
        { (ElementType.Fire, ElementType.Electric), 1f },
        { (ElementType.Fire, ElementType.Grass), 2f },
        { (ElementType.Fire, ElementType.Ice), 2f }, // Fire is super effective against Ice
        { (ElementType.Fire, ElementType.Fighting), 1f },
        { (ElementType.Fire, ElementType.Poison), 1f },
        { (ElementType.Fire, ElementType.Ground), 1f },
        { (ElementType.Fire, ElementType.Flying), 1f },
        { (ElementType.Fire, ElementType.Psychic), 1f },
        { (ElementType.Fire, ElementType.Bug), 2f }, // Fire is super effective against Bug
        { (ElementType.Fire, ElementType.Rock), 0.5f },
        { (ElementType.Fire, ElementType.Ghost), 1f },
        { (ElementType.Fire, ElementType.Dragon), 0.5f }, // Fire is not very effective against Dragon
        //Water
{ (ElementType.Water, ElementType.Normal), 1f },
        { (ElementType.Water, ElementType.Fire), 2f }, // Water is super effective against Fire
        { (ElementType.Water, ElementType.Water), 0.5f }, // Water resists Water
        { (ElementType.Water, ElementType.Electric), 1f },
        { (ElementType.Water, ElementType.Grass), 0.5f },
        { (ElementType.Water, ElementType.Ice), 1f },
        { (ElementType.Water, ElementType.Fighting), 1f },
        { (ElementType.Water, ElementType.Poison), 1f },
        { (ElementType.Water, ElementType.Ground), 2f }, // Water is super effective against Ground
        { (ElementType.Water, ElementType.Flying), 1f },
        { (ElementType.Water, ElementType.Psychic), 1f },
        { (ElementType.Water, ElementType.Bug), 1f },
        { (ElementType.Water, ElementType.Rock), 2f }, // Water is super effective against Rock
        { (ElementType.Water, ElementType.Ghost), 1f },
        { (ElementType.Water, ElementType.Dragon), 0.5f }, // Water is not very effective against Dragon
        //Electric
        { (ElementType.Electric, ElementType.Normal), 1f },
        { (ElementType.Electric, ElementType.Fire), 1f },
        { (ElementType.Electric, ElementType.Water), 1f },
        { (ElementType.Electric, ElementType.Electric), 1f },
        { (ElementType.Electric, ElementType.Grass), 1f },
        { (ElementType.Electric, ElementType.Ice), 1f },
        { (ElementType.Electric, ElementType.Fighting), 1f },
        { (ElementType.Electric, ElementType.Poison), 1f },
        { (ElementType.Electric, ElementType.Ground), 0.5f },
        { (ElementType.Electric, ElementType.Flying), 1f },
        { (ElementType.Electric, ElementType.Psychic), 1f },
        { (ElementType.Electric, ElementType.Bug), 1f },
        { (ElementType.Electric, ElementType.Rock), 1f },
        { (ElementType.Electric, ElementType.Ghost), 1f },
        { (ElementType.Electric, ElementType.Dragon), 1f },
        //Grass
        { (ElementType.Grass, ElementType.Normal), 1f },
        { (ElementType.Grass, ElementType.Fire), 0.5f },
        { (ElementType.Grass, ElementType.Water), 2f }, // Grass is super effective against Water
        { (ElementType.Grass, ElementType.Electric), 1f },
        { (ElementType.Grass, ElementType.Grass), 0.5f }, // Grass resists Grass
        { (ElementType.Grass, ElementType.Ice), 1f },
        { (ElementType.Grass, ElementType.Fighting), 1f },
        { (ElementType.Grass, ElementType.Poison), 0.5f },
        { (ElementType.Grass, ElementType.Ground), 2f },
        { (ElementType.Grass, ElementType.Flying), 0.5f }, // Grass is not very effective against Flying
        { (ElementType.Grass, ElementType.Psychic), 1f },
        { (ElementType.Grass, ElementType.Bug), 0.5f }, // Grass is not very effective against Bug
        { (ElementType.Grass, ElementType.Rock), 2f }, // Grass is super effective against Rock
        { (ElementType.Grass, ElementType.Ghost), 1f },
        { (ElementType.Grass, ElementType.Dragon), 0.5f }, // Grass is not very effective against Dragon
        //Ice
        { (ElementType.Ice, ElementType.Normal), 1f },
        { (ElementType.Ice, ElementType.Fire), 0.5f }, // Ice is not very effective against Fire
        { (ElementType.Ice, ElementType.Water), 0.5f }, // Ice is not very effective against Water
        { (ElementType.Ice, ElementType.Electric), 1f },
        { (ElementType.Ice, ElementType.Grass), 2f }, // Ice is super effective against Grass
        { (ElementType.Ice, ElementType.Ice), 0.5f }, // Ice resists Ice
        { (ElementType.Ice, ElementType.Fighting), 1f },
        { (ElementType.Ice, ElementType.Poison), 1f },
        { (ElementType.Ice, ElementType.Ground), 2f }, // Ice is super effective against Ground
        { (ElementType.Ice, ElementType.Flying), 2f }, // Ice is super effective against Flying
        { (ElementType.Ice, ElementType.Psychic), 1f },
        { (ElementType.Ice, ElementType.Bug), 1f },
        { (ElementType.Ice, ElementType.Rock), 2f }, // Ice is super effective against Rock
        { (ElementType.Ice, ElementType.Ghost), 1f },
        { (ElementType.Ice, ElementType.Dragon), 2f },
        // Fighting
        { (ElementType.Fighting, ElementType.Normal), 2f }, // Fighting is super effective against Normal
        { (ElementType.Fighting, ElementType.Fire), 1f },
        { (ElementType.Fighting, ElementType.Water), 1f },
        { (ElementType.Fighting, ElementType.Electric), 1f },
        { (ElementType.Fighting, ElementType.Grass), 1f },
        { (ElementType.Fighting, ElementType.Ice), 2f }, // Fighting is super effective against Ice
        { (ElementType.Fighting, ElementType.Fighting), 1f },
        { (ElementType.Fighting, ElementType.Poison), 0.5f }, // Fighting is not very effective against Poison
        { (ElementType.Fighting, ElementType.Ground), 1f },
        { (ElementType.Fighting, ElementType.Flying), 0.5f },
        { (ElementType.Fighting, ElementType.Psychic), 0.5f },
        { (ElementType.Fighting, ElementType.Bug), 0.5f }, // Fighting is not very effective against Bug
        { (ElementType.Fighting, ElementType.Rock), 2f },
        { (ElementType.Fighting, ElementType.Ghost), 0f }, // Fighting is ineffective against Ghost
        { (ElementType.Fighting, ElementType.Dragon), 1f },
        //Poison
      { (ElementType.Poison, ElementType.Normal), 1f },
        { (ElementType.Poison, ElementType.Fire), 1f },
        { (ElementType.Poison, ElementType.Water), 1f },
        { (ElementType.Poison, ElementType.Electric), 1f },
        { (ElementType.Poison, ElementType.Grass), 2f }, // Poison is super effective against Grass
        { (ElementType.Poison, ElementType.Ice), 1f },
        { (ElementType.Poison, ElementType.Fighting), 1f },
        { (ElementType.Poison, ElementType.Poison), 0.5f }, // Poison resists Poison
        { (ElementType.Poison, ElementType.Ground), 0.5f }, // Poison is not very effective against Ground
        { (ElementType.Poison, ElementType.Flying), 1f },
        { (ElementType.Poison, ElementType.Psychic), 1f },
        { (ElementType.Poison, ElementType.Bug), 1f },
        { (ElementType.Poison, ElementType.Rock), 0.5f }, // Poison is not very effective against Rock
        { (ElementType.Poison, ElementType.Ghost), 0.5f }, // Poison is not very effective against Ghost
        { (ElementType.Poison, ElementType.Dragon), 1f },
        //Ground
        { (ElementType.Ground, ElementType.Normal), 1f },
        { (ElementType.Ground, ElementType.Fire), 2f }, // Ground is super effective against Fire
        { (ElementType.Ground, ElementType.Water), 1f },
        { (ElementType.Ground, ElementType.Electric), 2f },
        { (ElementType.Ground, ElementType.Grass), 0.5f },
        { (ElementType.Ground, ElementType.Ice), 1f },
        { (ElementType.Ground, ElementType.Fighting), 1f },
        { (ElementType.Ground, ElementType.Poison), 2f }, // Ground is super effective against Poison
        { (ElementType.Ground, ElementType.Ground), 1f },
        { (ElementType.Ground, ElementType.Flying), 0f }, // Ground is ineffective against Flying
        { (ElementType.Ground, ElementType.Psychic), 1f },
        { (ElementType.Ground, ElementType.Bug), 0.5f }, // Ground is not very effective against Bug
        { (ElementType.Ground, ElementType.Rock), 2f },
        { (ElementType.Ground, ElementType.Ghost), 1f },
        { (ElementType.Ground, ElementType.Dragon), 1f },
        //Flying
        { (ElementType.Flying, ElementType.Normal), 1f },
        { (ElementType.Flying, ElementType.Fire), 1f },
        { (ElementType.Flying, ElementType.Water), 1f },
        { (ElementType.Flying, ElementType.Electric), 0.5f },
        { (ElementType.Flying, ElementType.Grass), 2f }, // Flying is super effective against Grass
        { (ElementType.Flying, ElementType.Ice), 1f },
        { (ElementType.Flying, ElementType.Fighting), 2f }, // Flying is super effective against Fighting
        { (ElementType.Flying, ElementType.Poison), 1f },
        { (ElementType.Flying, ElementType.Ground), 1f },
        { (ElementType.Flying, ElementType.Flying), 1f },
        { (ElementType.Flying, ElementType.Psychic), 1f },
        { (ElementType.Flying, ElementType.Bug), 2f },
        { (ElementType.Flying, ElementType.Rock), 0.5f },
        { (ElementType.Flying, ElementType.Ghost), 1f },
        { (ElementType.Flying, ElementType.Dragon), 1f },
        //Psychic
        { (ElementType.Psychic, ElementType.Normal), 1f },
        { (ElementType.Psychic, ElementType.Fire), 1f },
        { (ElementType.Psychic, ElementType.Water), 1f },
        { (ElementType.Psychic, ElementType.Electric), 1f },
        { (ElementType.Psychic, ElementType.Grass), 1f },
        { (ElementType.Psychic, ElementType.Ice), 1f },
        { (ElementType.Psychic, ElementType.Fighting), 2f }, // Psychic is super effective against Fighting
        { (ElementType.Psychic, ElementType.Poison), 2f }, // Psychic is super effective against Poison
        { (ElementType.Psychic, ElementType.Ground), 1f },
        { (ElementType.Psychic, ElementType.Flying), 1f },
        { (ElementType.Psychic, ElementType.Psychic), 0.5f }, // Psychic resists Psychic
        { (ElementType.Psychic, ElementType.Bug), 1f },
        { (ElementType.Psychic, ElementType.Rock), 1f },
        { (ElementType.Psychic, ElementType.Ghost), 0f }, // Psychic is ineffective against Ghost (pre-Gen VI)
        { (ElementType.Psychic, ElementType.Dragon), 1f },
        //Bug
        { (ElementType.Bug, ElementType.Normal), 1f },
        { (ElementType.Bug, ElementType.Fire), 0.5f },
        { (ElementType.Bug, ElementType.Water), 1f },
        { (ElementType.Bug, ElementType.Electric), 1f },
        { (ElementType.Bug, ElementType.Grass), 2f },
        { (ElementType.Bug, ElementType.Ice), 1f },
        { (ElementType.Bug, ElementType.Fighting), 0.5f },
        { (ElementType.Bug, ElementType.Poison), 0.5f },
        { (ElementType.Bug, ElementType.Ground), 1f },
        { (ElementType.Bug, ElementType.Flying), 0.5f },
        { (ElementType.Bug, ElementType.Psychic), 2f }, // Bug is super effective against Psychic
        { (ElementType.Bug, ElementType.Bug), 1f },
        { (ElementType.Bug, ElementType.Rock), 1f },
        { (ElementType.Bug, ElementType.Ghost), 0.5f }, // Bug is not very effective against Ghost
        { (ElementType.Bug, ElementType.Dragon), 1f },
         // Rock
        { (ElementType.Rock, ElementType.Normal), 1f },
        { (ElementType.Rock, ElementType.Fire), 2f }, // Rock is super effective against Fire
        { (ElementType.Rock, ElementType.Water), 1f },
        { (ElementType.Rock, ElementType.Electric), 1f },
        { (ElementType.Rock, ElementType.Grass), 1f },
        { (ElementType.Rock, ElementType.Ice), 2f }, // Rock is super effective against Ice
        { (ElementType.Rock, ElementType.Fighting), 0.5f },
        { (ElementType.Rock, ElementType.Poison), 1f },
        { (ElementType.Rock, ElementType.Ground), 0.5f }, // Rock is not very effective against Ground
        { (ElementType.Rock, ElementType.Flying), 2f }, // Rock is super effective against Flying
        { (ElementType.Rock, ElementType.Psychic), 1f },
        { (ElementType.Rock, ElementType.Bug), 2f }, // Rock is super effective against Bug
        { (ElementType.Rock, ElementType.Rock), 1f },
        { (ElementType.Rock, ElementType.Ghost), 1f },
        { (ElementType.Rock, ElementType.Dragon), 1f },
        // Ghost
        { (ElementType.Ghost, ElementType.Normal), 0f }, // Ghost is ineffective against Normal
        { (ElementType.Ghost, ElementType.Fire), 1f },
        { (ElementType.Ghost, ElementType.Water), 1f },
        { (ElementType.Ghost, ElementType.Electric), 1f },
        { (ElementType.Ghost, ElementType.Grass), 1f },
        { (ElementType.Ghost, ElementType.Ice), 1f },
        { (ElementType.Ghost, ElementType.Fighting), 1f },
        { (ElementType.Ghost, ElementType.Poison), 1f },
        { (ElementType.Ghost, ElementType.Ground), 1f },
        { (ElementType.Ghost, ElementType.Flying), 1f },
        { (ElementType.Ghost, ElementType.Psychic), 2f },
        { (ElementType.Ghost, ElementType.Bug), 1f },
        { (ElementType.Ghost, ElementType.Rock), 1f },
        { (ElementType.Ghost, ElementType.Ghost), 2f }, // Ghost is super effective against Ghost
        { (ElementType.Ghost, ElementType.Dragon), 1f },
        // Dragon
        { (ElementType.Dragon, ElementType.Normal), 1f },
        { (ElementType.Dragon, ElementType.Fire), 1f },
        { (ElementType.Dragon, ElementType.Water), 1f },
        { (ElementType.Dragon, ElementType.Electric), 1f },
        { (ElementType.Dragon, ElementType.Grass), 1f },
        { (ElementType.Dragon, ElementType.Ice), 1f },
        { (ElementType.Dragon, ElementType.Fighting), 1f },
        { (ElementType.Dragon, ElementType.Poison), 1f },
        { (ElementType.Dragon, ElementType.Ground), 1f },
        { (ElementType.Dragon, ElementType.Flying), 1f },
        { (ElementType.Dragon, ElementType.Psychic), 1f },
        { (ElementType.Dragon, ElementType.Bug), 1f },
        { (ElementType.Dragon, ElementType.Rock), 1f },
        { (ElementType.Dragon, ElementType.Ghost), 1f },
        { (ElementType.Dragon, ElementType.Dragon), 2f },
    };

    public static float GetEffectiveness(ElementType attack, ElementType defense)
    {
        return chart.GetValueOrDefault((attack, defense), 1f);
    }

    public static float CalculateEffectiveness(ElementType attack, List<ElementType> defenderTypes)
    {
        float totalEffectiveness = 1f;
        foreach (var defenderType in defenderTypes)
        {
            totalEffectiveness *= GetEffectiveness(attack, defenderType);
        }
        return totalEffectiveness;
    }

}
