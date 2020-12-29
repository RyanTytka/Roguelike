using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : ActingUnit
{
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    public float attack;
    public float magic;
    public float defense;
    public float resilience;
    public float speed;

    public void RandomizeStats()
    {
        gameObject.SetActive(false);
        //add to a random stat 10 times
        for (int i = 0; i < 10; i++)
        {
            int stat = Random.Range(1, 8);
            if (stat == 1)
                maxHealth += Random.Range(1,3);
            else if (stat == 2)
                maxMana += Random.Range(1, 3);
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
        currentHealth = maxHealth;
        currentMana = maxMana;
    }

    public void AddStats(int[] newStats)
    {
        maxHealth += newStats[0] * 2;
        currentHealth += newStats[0] * 2;
        maxMana += newStats[1] * 2;
        currentMana += newStats[1] * 2;
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
        //Debug.Log("Player Turn");
        //highlight me
        GetComponent<SpriteRenderer>().color = Color.yellow;
        //display my moves
        PlayerAbilities abilities = GetComponent<PlayerAbilities>();
        abilities.Display();
    }

    public override void EndTurn()
    {
        //unhighlight me
        GetComponent<SpriteRenderer>().color = Color.white;
        //remove my ability buttons
        PlayerAbilities abilities = GetComponent<PlayerAbilities>();
        abilities.Hide();
    }

    //update mana and health bar with current values
    public void SetBars()
    {
        GetComponentInChildren<HealthBar>().CurrentValue = currentHealth;
        GetComponentInChildren<HealthBar>().MaxValue = maxHealth;
        GetComponentInChildren<ManaBar>().CurrentValue = currentMana;
        GetComponentInChildren<ManaBar>().MaxValue = maxMana;
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        SetBars();
    }
}
