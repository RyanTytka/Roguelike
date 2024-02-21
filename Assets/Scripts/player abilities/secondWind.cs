using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondWind : AbilityInterface
{
    void Update()
    {
        TargetSelf();
    }

    public override void Use()
    {
        //heal
        caster.GetComponent<PlayerStats>().Heal(caster.GetComponent<PlayerStats>().Resilience);
        //recover mana 
        caster.GetComponent<PlayerStats>().RecoverMana(caster.GetComponent<PlayerStats>().Resilience);

        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Second Wind.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string res;
        if (caster == null)
        {
            res = "(Resilience)";
        }
        else
        {
            res = caster.GetComponent<PlayerStats>().Resilience.ToString();
        }
        return "Recover " + res + " Mana and Health.";
    }
}
