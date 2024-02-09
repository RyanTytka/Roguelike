using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is used to display the end of each round in the turn tracker
public class roundDivider : ActingUnit
{
    public Sprite sprite;

    //adds to turn timer and returns true if their turn timer has been filled
    public override bool UpdateTurn()
    {
        turnTimer += 10;
        if (turnTimer >= 100)
        {
            turnTimer -= 100;
            return true;
        }
        return false;
    }

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
                    case BLEEDING:
                        unit.TakeDamage(unit.GetComponent<PlayerStats>().health * 0.02f * se.stacks, 3);
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
