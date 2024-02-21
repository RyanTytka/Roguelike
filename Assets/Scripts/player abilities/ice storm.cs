using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icestorm : AbilityInterface
{
    void Update()
    {
        TargetAllEnemies();
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Magic * 0.5f, 1);
            CreateStatusEffect(StatusTypeEnum.SPEED_DOWN, 2, 0, obj);
        }
        
        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses ice storm.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string mgc;
        if (caster == null)
        {
            mgc = "(Magic * 0.5)";
        }
        else
        {
            mgc = (string)(caster.GetComponent<PlayerStats>().Magic * 0.5f);
        }
        return "Deal " + mgc + " magic damage to all enemies and apply Speed Down 2 to them.";
    }
}
