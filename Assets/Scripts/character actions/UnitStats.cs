using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStats : ActingUnit, IComparable
{
    public float maxHealth;
    public float currentHealth;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;


    private bool dead = false;

    public void calculateNextActTurn(int currentTurn)
    {
        this.turnTimer = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }

    public int CompareTo(object otherStats)
    {
        return turnTimer.CompareTo(((UnitStats)otherStats).turnTimer);
    }

    public bool isDead()
    {
        return this.dead;
    }

    //adds to turn timer and returns true if their turn timer has been filled
    public override bool UpdateTurn()
    {
        turnTimer += speed;
        if (turnTimer >= 100)
        {
            turnTimer -= 100;
            return true;
        }
        return false;
    }

    public override void MyTurn()
    {
        //Debug.Log("Enemy Turn");
        
        StartCoroutine(Wait());

    }

    IEnumerator Wait()
    {
        //highlight me
        GetComponent<SpriteRenderer>().color = Color.yellow;

        //pause
        yield return new WaitForSeconds(0.5f);
        
        //use an ability
        GetComponent<Enemy>().TakeTurn();

        yield return new WaitForSeconds(0.5f);

        //end turn
        GameObject.Find("GameManager").GetComponent<BattleManager>().TurnEnded();
    }

    public override void EndTurn()
    {
        if(!dead)
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    //update health bar with current heralth value
    public void SetHealthBar()
    {
        GetComponentInChildren<HealthBar>().CurrentValue = currentHealth;
        GetComponentInChildren<HealthBar>().MaxValue = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SetHealthBar();

        if(currentHealth <= 0)
        {
            dead = true;
            GameObject.Find("GameManager").GetComponent<BattleManager>().battlingUnits.Remove(this.gameObject);
            GameObject.Find("TurnTracker").GetComponent<TurnTracker>().UnitDied(this.gameObject);
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }

}
