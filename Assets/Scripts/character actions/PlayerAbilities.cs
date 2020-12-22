using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    public List<AbilityInterface> abilities = new List<AbilityInterface>();

    void Start()
    {
        
    }

    public void LearnAbility(AbilityInterface ability)
    {
        abilities.Add(ability);
    }
}
