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

    //display info and act when it is my turn
    public override void MyTurn()
    {
        //next round
        //(currently handled in turn tracker class)

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
