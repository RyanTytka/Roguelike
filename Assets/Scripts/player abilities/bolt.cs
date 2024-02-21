using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bolt : AbilityInterface
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
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Bolt.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Magic)";
        }
        else
        {
            atk = caster.GetComponent<PlayerStats>().Magic.ToString();
        }
        return "Fire a bolt of magic at an enemy, dealing " + atk + " magical damage.";
    }
}
