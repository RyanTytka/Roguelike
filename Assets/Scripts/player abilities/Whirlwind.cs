using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : AbilityInterface
{
    public override void Use()
    {
        foreach(GameObject obj in targets)
        {
            //deal damage
        }
    }
}
