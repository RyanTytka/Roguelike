using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        int abilityNum = Random.Range(0, possibleAbilities.Count);
        possibleAbilities[abilityNum].GetComponent<AbilityInterface>().Use();
    }
}
