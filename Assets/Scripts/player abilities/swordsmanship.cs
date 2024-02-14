using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordsmanship : AbilityInterface
{
    public override string GetDescription()
    {
        return "Your Basic abilities deal 25% more damage.";
    }

    public override void Use() { } //This is a passive ability
}
