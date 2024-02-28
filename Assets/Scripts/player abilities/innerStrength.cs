using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class innerStrength : AbilityInterface
{
    void Update()
    {
        TargetSelf();
    }

    public override void Use()
    {
        CreateStatusEffect(StatusTypeEnum.STRENGTH_UP, 3, caster);
        
        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Inner Strength.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        return "Double your stacks of Strength Up.";
    }
}
