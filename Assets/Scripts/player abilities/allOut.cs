using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : AbilityInterface
{
    void Update()
    {
        TargetAnEnemy();
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Attack * 2, 1);
        }
        CreateStatusEffect(StatusTypeEnum.STRENGTH_DOWN, 2, 0, caster);
        
        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Shield Bash.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string atk;
        if (caster == null)
        {
            atk = "(Str * 2)";
        }
        else
        {
            atk = (caster.GetComponent<PlayerStats>().Attack * 2).ToString();
        }
        return "Attack an enemy, dealing " + atk + " physical damage and gaining Strength Down 2.";
    }
}
