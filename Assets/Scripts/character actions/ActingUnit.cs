using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActingUnit : MonoBehaviour
{
    public float turnTimer;

    private void Update()
    {
        //display health bar

    }

    //adds to turn timer and returns true if their turn timer has been filled
    public abstract bool UpdateTurn();

    //display info and act when it is my turn
    public abstract void MyTurn();
    
    //clean up ability icons and remove highlight after my turn is over
    public abstract void EndTurn();

    //update status effect icoins on this unit
    public abstract void UpdateStatusEffects();

    //gets multipliers for each stat when accounting for status effects
    public float[] StatusEffectMods()
    {
        //0 - MaxHealth
        //1 - MaxMana
        //2 - ManaRegen
        //3 - Strength
        //4 - Magic
        //5 - Armor
        //6 - Resilience
        //7 - Speed
        float[] mods = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };

        var statusEffects = GetComponentsInChildren<StatusEffect>();
        foreach (StatusEffect statusEffect in statusEffects)
        {
            switch (statusEffect.type)
            {
                case StatusType.MANAREGEN_UP:
                    mods[2] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.MANAREGEN_DOWN:
                    mods[2] *= 0.8f ^ statusEffect.stacks;
                    break;
                case StatusType.STRENGTH_UP:
                    mods[3] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.STRENGTH_DOWN:
                    mods[3] *= 0.8f ^ statusEffect.stacks;
                    break;
                case StatusType.MAGIC_UP:
                    mods[4] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.MAGIC_DOWN:
                    mods[4] *= 0.8f ^ statusEffect.stacks;
                    break;
                case StatusType.ARMOR_UP:
                    mods[5] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.ARMOR_DOWN:
                    mods[5] *= 0.8f ^ statusEffect.stacks;
                    break;
                case StatusType.RES_UP:
                    mods[6] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.RES_DOWN:
                    mods[6] *= 0.8f ^ statusEffect.stacks;
                    break;
                case StatusType.SPEED_UP:
                    mods[7] += 0.25f * statusEffect.stacks;
                    break;
                case StatusType.SPEED_DOWN:
                    mods[7] *= 0.8f ^ statusEffect.stacks;
                    break;
            }
        }

        return mods;
    }
}
