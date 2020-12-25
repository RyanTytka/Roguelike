using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whack : AbilityInterface
{
    public override void Use()
    {
        Debug.Log("whack used");

        foreach (GameObject obj in targets)
        {
            //deal damage
        }
    }
}
