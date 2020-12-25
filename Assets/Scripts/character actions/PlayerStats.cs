using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : ActingUnit
{
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;


    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gameObject.SetActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //gameObject.SetActive(scene.name == "Battle");
    }

    public void RandomizeStats()
    {
        //add to a random stat 10 times
        for (int i = 0; i < 10; i++)
        {
            int stat = Random.Range(1, 8);
            if (stat == 1)
                health += Random.Range(1,3);
            else if (stat == 2)
                mana += Random.Range(1, 3);
            else if (stat == 3)
                attack += 1;
            else if (stat == 4)
                magic += 1;
            else if (stat == 5)
                defense += 1;
            else if (stat == 6)
                resilience += 1;
            else if (stat == 7)
                speed += Random.Range(1, 3);

        }
    }

    public void AddStats(int[] newStats)
    {
        health += newStats[0] * 2;
        mana += newStats[1] * 2;
        attack += newStats[2];
        magic += newStats[3];
        defense += newStats[4];
        resilience += newStats[5];
        speed += newStats[6] * 2;
    }

    //adds to turn timer and returns true if their turn timer has been filled
    public override bool UpdateTurn()
    {
        turnTimer += speed;
        if(turnTimer >= 100)
        {
            turnTimer -= 100;
            return true;
        }
        return false;
    }

    public override void MyTurn()
    {
        //display my moves
        PlayerAbilities abilities = GetComponent<PlayerAbilities>();
        abilities.Display();
    }

}
