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
        //3 - Attack
        //4 - Magic
        //5 - Defense
        //6 - Resilience
        //7 - Speed
        float[] mods = new float[] { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f };

        var statusEffects = GetComponentsInChildren<StatusEffect>();
        foreach (StatusEffect statusEffect in statusEffects)
        {
            switch (statusEffect.type)
            {
                case StatusType.STRENGTH:
                    mods[3] += statusEffect.tierPercent;
                    break;
            }
        }

        return mods;
    }
}
