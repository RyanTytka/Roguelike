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
    //private int round; //what round it is (starts at 1)

    public GameObject roundDivider; //adds this to acting units to progress its turn and keep track of the round
    public Text roundText;
    public Slider roundSlider;

    //set up first 5 turns
    public void Init()
    {
        //round = 1;

        InitRound();
        allUnits.Add(roundDivider);
        turnOrder.Add(roundDivider);
    }

    //Get turn order for a round
    public void InitRound()
    {
        //find highest speed to get its speed tier
        float currentHighestSpeed = 0;
        foreach (GameObject go in allUnits) 
        {
            if(go.GetComponent<ActingUnit>().Speed > currentHighestSpeed)
                currentHighestSpeed = go.GetComponent<ActingUnit>().Speed;
        }
        int Div15 = (int)(currentHighestSpeed / 15);
        float lastHighestSpeed = 9999;
        do //go through each speed tier (increments of 15)
        {
            do //get each speed in this speed tier
            {
                currentHighestSpeed = 0;
                foreach (GameObject go in allUnits) //find next highest speed
                {
                    if (go.GetComponent<ActingUnit>().Speed - Div15 * 15 > 0 &&
                        go.GetComponent<ActingUnit>().Speed % 15 > currentHighestSpeed &&
                        go.GetComponent<ActingUnit>().Speed % 15 < lastHighestSpeed)
                    {
                        currentHighestSpeed = go.GetComponent<ActingUnit>().Speed % 15;
                    }
                }
                if (currentHighestSpeed > 0)
                {
                    lastHighestSpeed = currentHighestSpeed;
                    // Add all units with that speed
                    foreach (GameObject go in allUnits)
                    {
                        if(go.GetComponent<ActingUnit>().Speed % 15 == currentHighestSpeed)
                        turnOrder.Add(go);
                    }
                }
            } while (currentHighestSpeed > 0);
            Div15--;
            lastHighestSpeed = 9999;
        } while (Div15 >= 0); //keep looping until all units have been added
    }

    //moves turn order up 1 or starts new round 
    public GameObject NextTurn()
    {
        //current turn has ended, so remove them from turn order
        if(!firstTurn)
            turnOrder.RemoveAt(0);
        firstTurn = false;

        if(turnOrder.Count == 0)
            InitRound();

        //clear existing turn sprites
        while(turnImages.Count > 0)
        {
            turnImages.RemoveAt(0);
        }
        //add sprites back in turn order
        Display();

        //return current turn
        return turnOrder[0];
    }

    //when a unit dies, call this to remove it from turn order
    public void UnitDied(GameObject unit)
    {
        while (turnOrder.Remove(unit))
        { 
            //print("removed"); 
        }
        //restructure unit order
        GameObject.Find("GameManager").GetComponent<BattleManager>().UpdateUnitPositions();
    }

    // Add a sprite for each unit in the turn order
    public void Display()
    {
        for(int i = 0; i < turnOrder.Count; i++)
        {
            turnImages.Add(Instantiate(turnOrderPrefab, i * 100, 0, this.gameObject));
            turnImages[i].GetComponent<Image>().sprite = turnOrder[i].GetComponent<SpriteRenderer>().sprite;
        }
    }
}
