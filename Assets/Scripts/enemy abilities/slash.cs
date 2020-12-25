using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slash : AbilityInterface
{
    public override void Use()
    {
        Debug.Log("slash used");

        foreach (GameObject obj in targets)
        {
            //deal damage
        }
    }
}
