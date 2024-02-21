using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash : AbilityInterface
{
    Ray ray;
    RaycastHit hit;

    void Update()
    {
        TargetAnEnemy();
    }

    public override void Use()
    {
        float dmg = caster.GetComponent<PlayerStats>().Attack;
        if(caster.GetComponentsInChildren<swordsmanship>() != null)
            dmg *= 1.25f;
        foreach (GameObject obj in targets)
        {
            obj.GetComponent<UnitStats>().TakeDamage(dmg, (int)DamageTypesEnum.PHYSICAL);
            if(caster.GetComponentsInChildren<crushingBlows>() != null)
                CreateStatusEffect(StatusTypeEnum.ARMOR_DOWN, 1, 0, obj);
        }

        //clear targets and end turn
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Slash.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string str;
        if (caster == null)
        {
            str = "(Strength)";
        }
        else
        {
            str = caster.GetComponent<PlayerStats>().Strength.ToString();
        }
        return "Slash an enemy, dealing " + str + " physical damage.";
    }
}
