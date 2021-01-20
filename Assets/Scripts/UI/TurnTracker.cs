using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnTracker : MonoBehaviour
{
    public List<GameObject> allUnits;
    public List<GameObject> turnOrder = new List<GameObject>();  
    public List<GameObject> turnImages; //reference to the image objects that display turn order
    private bool firstTurn = true;

    //set up first 5 turns
    public void Init()
    {
        //get all units in battle
        //allUnits.AddRange(GameObject.Find("PlayerParty").GetComponent<PartyManager>().party);

        //calculate first 5 turns
        while(turnOrder.Count < 5)
        {
            foreach (GameObject go in allUnits)
            {
                if (go.GetComponent<ActingUnit>().UpdateTurn())
                    turnOrder.Add(go);
            }
        }
    }

    //moves turn order up 1 and calculates next turn
    public GameObject NextTurn()
    {
        //current turn has ended, so remove them from turn order
        if(!firstTurn)
            turnOrder.RemoveAt(0);
        firstTurn = false;

        int c = 0;
        //add to end of turnOrder
        while (turnOrder.Count < 5 && c < 100)
        {
            foreach (GameObject go in allUnits)
            {
                if (go.GetComponent<ActingUnit>().UpdateTurn())
                    turnOrder.Add(go);
            }
            c++;
        }

        //update turn tracker UI
        Display();

        //return current turn
        return turnOrder[0];
    }

    //when a unit dies, call this to remove it from turn order
    public void UnitDied(GameObject unit)
    {
        while (turnOrder.Remove(unit)) { print("removed"); }
        //refill turnOrder
        int c = 0;
        while (turnOrder.Count < 5 && c < 100)
        {
            foreach (GameObject go in allUnits)
            {
                if (go.GetComponent<ActingUnit>().UpdateTurn())
                    turnOrder.Add(go);
            }
            c++;
        }
    }

    public void Display()
    {
        for(int i = 0; i < 5 && i < turnOrder.Count; i++)
        {
            turnImages[i].GetComponent<Image>().sprite = turnOrder[i].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
