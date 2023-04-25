using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondWind : AbilityInterface
{
    public override void Use()
    {
        //heal self

        //Update History
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(caster.GetComponent<PlayerStats>().playerName + " uses Second Wind.");
    }

    public override string GetDescription()
    {
        string mana;
        string res;
        if (caster == null)
        {
            mana = "(Mana Regen)";
            res = "(Resilience)";
        }
        else
        {
            mana = caster.GetComponent<PlayerStats>().ManaRegen.ToString();
            res = caster.GetComponent<PlayerStats>().Resilience.ToString();
        }
        return "Recover " + mana + " mana and heal for " + res + " health.";
    }
}
