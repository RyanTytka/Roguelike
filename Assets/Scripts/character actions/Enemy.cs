using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public List<GameObject> possibleAbilities;
    public float difficulty;

    private void Start()
    {
        GenerateAbilities();
    }
    public void GenerateAbilities()
    {

    }
}
