using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStats : ActingUnit, IComparable
{
    public float health;
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

}
