using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilityManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    public List<GameObject> allAbilities;

    public List<GameObject> newAbility()
    {
        List<GameObject> temp = new List<GameObject>();

        temp.Add(allAbilities[0]);
        temp.Add(allAbilities[0]);
        temp.Add(allAbilities[0]);

        return temp;
    }
}
