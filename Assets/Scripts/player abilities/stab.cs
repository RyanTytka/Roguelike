using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stab : AbilityInterface
{
    void Update()
    {
        TargetAnEnemy();
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Attack, 1); 
        }

        //clear targets and end turn
        AbilityUsed();
        
        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Stab.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Attack)";
        }
        else
        {
            atk = caster.GetComponent<PlayerStats>().Attack.ToString();
        }
        return "Stab an enemy, dealing " + atk + " physical damage.";
    }
}
