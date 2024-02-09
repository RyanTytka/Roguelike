using System.Collections;
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
        //need a system for enemies that have more specific action sequences. maybe:
        //if(I am a zombie)
        //{ Choose from Zombie abilities }
        int abilityNum = Random.Range(0, possibleAbilities.Count);
        possibleAbilities[abilityNum].GetComponent<AbilityInterface>().caster = this.gameObject;
        possibleAbilities[abilityNum].GetComponent<AbilityInterface>().Use();
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
