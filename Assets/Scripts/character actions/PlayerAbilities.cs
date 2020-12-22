using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public List<GameObject> abilities = new List<GameObject>();

    void Start()
    {
        
    }

    public void LearnAbility(GameObject ability)
    {
        abilities.Add(ability);
    }
}
