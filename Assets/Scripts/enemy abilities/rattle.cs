﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rattle : AbilityInterface
{
    //give all enemy units strength I for 2 rounds
    public override void Use()
    {
        print("Rattle used");
        
        //targets all enemy units
        targets.Clear();
        BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
        foreach (GameObject unit in bm.battlingUnits)
        {
            if(unit.tag == "Enemy")
            {
                targets.Add(unit);
            }
        }

        foreach (GameObject target in targets)
        {
            //add strength status effect
            CreateStatusEffect(StatusTypeEnum.STRENGTH_UP, 2, target);
        }
    }

    public override string GetDescription()
    {
        return "Strengthen allies";
    }
}