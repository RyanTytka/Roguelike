using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : AbilityInterface
{
    private void Update()
    {
        if(selected)
        {

        }
    }

    public override void Use()
    {
        Debug.Log("Whirlwind used");
        foreach(GameObject obj in targets)
        {
            //deal damage
        }
    }
}
