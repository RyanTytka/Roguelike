using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adaptiveFighting : AbilityInterface
{
    public override string GetDescription()
    {
        return "Gain 2 Armor after getting hit by an attack.";
    }

    public override void Use() { } //This is a passive ability
}
