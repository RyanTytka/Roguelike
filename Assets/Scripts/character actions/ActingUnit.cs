using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActingUnit : MonoBehaviour
{
    public string unitName, description;
    public float maxHealth;
    public float currentHealth;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;

    //get stats that take status effects into account
    public virtual float MaxHealth { get { return maxHealth * StatusEffectMods()[0]; } }
    public virtual float Attack { get { return (attack + attackMod) * StatusEffectMods()[3]; } }
    public virtual float Magic { get { return (magic + magicMod) * StatusEffectMods()[4]; } }
    public virtual float Defense { get { return (defense + defenseMod) * StatusEffectMods()[5]; } }
    public virtual float Resilience { get { return (resilience + resMod) * StatusEffectMods()[6]; } }
    public virtual float Speed { get { return (speed + speedMod) * StatusEffectMods()[7]; } }

    // flat temp bonuses to stats
    public float attackMod;
    public float magicMod;
    public float defenseMod;
    public float resMod;
    public float speedMod;
    public float manaRegenMod;

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
                case StatusTypeEnum.MANAREGEN_UP:
                    mods[2] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.MANAREGEN_DOWN:
                    mods[2] *= Mathf.Pow(0.8f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.STRENGTH_UP:
                    mods[3] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.STRENGTH_DOWN:
                    mods[3] *= Mathf.Pow(0.8f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.MAGIC_UP:
                    mods[4] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.MAGIC_DOWN:
                    mods[4] *= Mathf.Pow(0.8f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.ARMOR_UP:
                    mods[5] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.ARMOR_DOWN:
                    mods[5] *= Mathf.Pow(0.8f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.RES_UP:
                    mods[6] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.RES_DOWN:
                    mods[6] *= Mathf.Pow(0.8f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.SPEED_UP:
                    mods[7] += Mathf.Pow(0.25f, statusEffect.stacks);
                    break;
                case StatusTypeEnum.SPEED_DOWN:
                    mods[7] *= Mathf.Pow(0.8f,statusEffect.stacks);
                    break;
            }
        }

        return mods;
    }

    //Heal this unit for specificed amount, affected by modifiers
    public void Heal(float amount)
    {
        var statusEffects = gameObject.GetComponentsInChildren<StatusEffect>();
        foreach(StatusEffect se in statusEffects)
        {
            if(se.type == StatusTypeEnum.POISONED)
            {
                amount *= 0.5;
            }
            if(se.type == StatusTypeEnum.BLEEDING)
            {
                Destroy(se);
            }
        }
        gameObject.GetComponent<PlayerStats>().currentHealth += amount;
        gameObject.GetComponent<PlayerStats>().currentHealth = Mathf.Min(gameObject.GetComponent<PlayerStats>().currentHealth, gameObject.GetComponent<PlayerStats>().MaxHealth);
    }

    //Recover amount mana, up to max
    public void RecoverMana(float amount)
    {
        gameObject.GetComponent<PlayerStats>().currentMana += amount;
        gameObject.GetComponent<PlayerStats>().currentMana = Mathf.Min(gameObject.GetComponent<PlayerStats>().currentMana, gameObject.GetComponent<PlayerStats>().MaxMana);
    }
}
