using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is used to display the end of each round in the turn tracker
public class roundDivider : ActingUnit
{
    public Sprite sprite;

    // trigger round end effects
    public override void MyTurn()
    {
        BattleManager bm = GameObject.Find("GameManager").GetComponent<BattleManager>();
        //status effects
        foreach (GameObject unit in bm.battlingUnits)
        {
            var statusEffects = unit.GetComponentsInChildren<StatusEffect>();
            foreach(StatusEffect se in statusEffects)
            {
                switch(se.type)
                {
                    case StatusTypeEnum.BLEEDING:
                        unit.GetComponent<UnitStats>().TakeDamage(unit.GetComponent<PlayerStats>().currentHealth * 0.02f * se.stacks, 3);
                    case StatusTypeEnum.BURNING:
                        unit.GetComponent<UnitStats>().TakeDamage(1 * se.stacks, 2);
                        se.stacks--;
                    break;
                }
                se.Progress();
            }
        }

        //go to next unit's turn
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
    }

    //clean up ability icons and remove highlight after my turn is over
    public override void EndTurn()
    {

    }

    public override void UpdateStatusEffects()
    {
        //does nothing
    }
}
