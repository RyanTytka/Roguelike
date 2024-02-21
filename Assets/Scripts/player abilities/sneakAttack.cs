using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sneakAttack : AbilityInterface
{
    void Update()
    {
        TargetAnEnemy();
    }

    public override void Use()
    {
        foreach (GameObject obj in targets)
        {
            //deal damage
        }

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses sneak attack.";
        GameObject.Find("History").GetComponent<BattleHistory>().AddLog(history);
    }

    public override string GetDescription()
    {
        string spd;
        if (caster == null)
        {
            spd = "(Speed)";
        }
        else
        {
            spd = caster.GetComponent<PlayerStats>().Speed.ToString();
        }
        return "Deal " + spd + " physical damage.";
    }
}
