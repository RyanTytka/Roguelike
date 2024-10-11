using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frenzy : AbilityInterface
{
    void Update()
    {
        TargetAllEnemies();
    }

    public override void Use()
    {
        for(int i = 0; i < 3; i++)
        {
            //choose a random enemy
            int selectedEnemy = Random.Range(0, targets.Count);
            targets[selectedEnemy].GetComponent<UnitStats>().TakeDamage(caster.GetComponent<PlayerStats>().Attack, 1);
        }

        //clear targets
        AbilityUsed();

        //Update History
        string history = caster.GetComponent<PlayerStats>().playerName + " uses Frenzy.";
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
            str = caster.GetComponent<PlayerStats>().Attack.ToString();
        }
        return "Attack a random enemy three times, dealing " + str + " physical damage each.";
    }
}
