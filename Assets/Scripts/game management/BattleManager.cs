using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    public Encounter encounter;

    private List<GameObject> battlingUnits;
    private GameObject currentTurn;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Battle")
        {
            encounter = GetComponentInChildren<Encounter>();

            //repeat until all enemies or players have died
            while (BattleStillGoing())
            {
                //one round of turns
                List<GameObject> actingUnits = nextTurn();
                int currentTurn = 0;
                //while (actingUnits.Count > 0)
                //{
                //    actingUnits[currentTurn].GetComponent<ActingUnit>().MyTurn();
                //    currentTurn++;
                //}
            }

            //end of battle
        }
    }

    //when a unit has taken its turn, this is called and moves it to the next units turn
    public void TurnEnded()
    {

    }

    //find the unit(s) that act next
    private List<GameObject> nextTurn()
    {
        List<GameObject> turns = new List<GameObject>();
        foreach (GameObject go in battlingUnits)
        {
            if (go.GetComponent<ActingUnit>().UpdateTurn())
                turns.Add(go);
        }

        return turns;
    }

    //returns true unless all enemies or players have died
    private bool BattleStillGoing()
    {
        int pCount = 0, eCount = 0;
        foreach(GameObject go in battlingUnits)
        {
            if (go.tag == "Player")
            {
                pCount++;
            }
            else
            {
                eCount++;
            }
        }
        return pCount != 0 && eCount != 0;
    }
}
