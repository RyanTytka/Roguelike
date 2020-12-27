using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActingUnit : MonoBehaviour
{
    public float turnTimer;

    private void Update()
    {
        //display health bar

    }

    //adds to turn timer and returns true if their turn timer has been filled
    public abstract bool UpdateTurn();

    //display info and act when it is my turn
    public abstract void MyTurn();
    //clean up ability icons and remove highlight after my turn is over
    public abstract void EndTurn();

}
