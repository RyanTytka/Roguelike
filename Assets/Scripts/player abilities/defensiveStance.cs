using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defensiveStance : AbilityInterface
{
    void Update()
    {
        TargetSelf();
    }

    public override void Use()
    {
        CreateStatusEffect(StatusTypeEnum.ARMOR_UP, 3, caster);
        CreateStatusEffect(StatusTypeEnum.RES_UP, 3, caster);
        CreateStatusEffect(StatusTypeEnum.STRENGTH_DOWN, 2, caster);
        
        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Defensive Stance.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        return "Gain Armor Up 3 and Res Up 3, but gain Strength Down 2.";
    }
}
