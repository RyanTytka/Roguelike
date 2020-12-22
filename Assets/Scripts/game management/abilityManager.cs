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

    //returns 3 random abilities from the list of all abilities
    public List<GameObject> newAbility()
    {
        List<GameObject> temp = new List<GameObject>();
        List<int> used = new List<int>();
        int index;

        index = Random.Range(0, allAbilities.Count);
        temp.Add(allAbilities[index]);
        used.Add(index);

        while (used.Contains(index))
            index = Random.Range(0, allAbilities.Count);
        used.Add(index);
        temp.Add(allAbilities[index]);

        while (used.Contains(index))
            index = Random.Range(0, allAbilities.Count);
        used.Add(index);
        temp.Add(allAbilities[index]);

        return temp;
    }
}
