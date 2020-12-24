using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActingUnit : MonoBehaviour
{
    public float turnTimer;

    //adds to turn timer and returns true if their turn timer has been filled
    public abstract bool UpdateTurn();

    //display info and act when it is my turn
    public abstract void MyTurn();
}
