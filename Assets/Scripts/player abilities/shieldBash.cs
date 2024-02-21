using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldBash : AbilityInterface
{
    void Update()
    {
        TargetAnEnemy();
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Defense, 1);
        }
        
        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Shield Bash.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string armor;
        if (caster == null)
        {
            armor = "(Armor)";
        }
        else
        {
            armor = caster.GetComponent<PlayerStats>().Defense.ToString();
        }
        return "Bash an enemy with your shield, dealing " + armor + " physical damage.";
    }
}
