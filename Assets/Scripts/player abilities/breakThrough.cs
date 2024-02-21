using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakThrough : AbilityInterface
{
    void Update()
    {
        TargetSelf();
    }

    public override void Use()
    {
        //clear my debuffs
        var effects = caster.GetComponentsInChildren<StatusEffect>();
        foreach (StatusEffect se in effects)
        {
            if(se.IsDebuff[(int)se.type] == false)
            {
                Destroy(se);
            }
        }
        CreateStatusEffect(StatusTypeEnum.ARMOR_UP, 2, 0, caster);
        CreateStatusEffect(StatusTypeEnum.RES_UP, 2, 0, caster);

        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Break Through.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        return "Remove all debuffs and apply Armor Up 2 and Resilience Up 2.";
    }
}
