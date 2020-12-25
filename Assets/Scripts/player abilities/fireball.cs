using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : AbilityInterface
{
    public override void Use()
    {
        Debug.Log("Fireball used");
        foreach (GameObject obj in targets)
        {
            //deal damage
        }
    }
}
