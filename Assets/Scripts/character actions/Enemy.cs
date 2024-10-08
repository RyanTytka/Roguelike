﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public List<GameObject> possibleAbilities;
    public float difficulty;
    public bool isBoss;

    private void Start()
    {
        GenerateAbilities();
    }
    public void GenerateAbilities()
    {

    }

    public void TakeTurn()
    {
        if (GetComponent<ActingUnit>().unitName == "Cultist")
        {
            int abilityNum = Random.Range(0, possibleAbilities.Count);
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().caster = this.gameObject;
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().Use();
        }
        if (GetComponent<ActingUnit>().unitName == "Tentacle")
        {
            int abilityNum = Random.Range(0, possibleAbilities.Count);
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().caster = this.gameObject;
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().Use();
        }
        else
        {
            int abilityNum = Random.Range(0, possibleAbilities.Count);
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().caster = this.gameObject;
            possibleAbilities[abilityNum].GetComponent<AbilityInterface>().Use();
        }
    }

    void OnMouseOver()
    {
        GetComponentInChildren<Text>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Text>().enabled = false;
    }
}
