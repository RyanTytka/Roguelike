using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitStats : MonoBehaviour, IComparable
{
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;

    public int nextActTurn;

    private bool dead = false;

    public void calculateNextActTurn(int currentTurn)
    {
        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }

    public int CompareTo(object otherStats)
    {
        return nextActTurn.CompareTo(((UnitStats)otherStats).nextActTurn);
    }

    public bool isDead()
    {
        return this.dead;
    }

}
