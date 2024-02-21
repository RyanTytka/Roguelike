using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : AbilityInterface
{
    void Update()
    {
        //if (GetComponent<PlayerStats>().currentMana >= ability.GetComponent<AbilityInterface>().manaCost)
        {

        }
        TargetAllEnemies();
    }

    public override void Use()
    {
        caster.GetComponent<PlayerStats>().UseMana(manaCost);
        foreach(GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Attack, 1);
        }

        //clear targets and end turn
        AbilityUsed();
        
        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Whirlwind.";
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
        return "Deal " + atk + " physical damage to each enemy.";
    }
}
