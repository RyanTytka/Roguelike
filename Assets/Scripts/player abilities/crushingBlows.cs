using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crushingBlows : AbilityInterface
{
    public override string GetDescription()
    {
        return "Your Basic abilities apply Armor Down 1.";
    }

    public override void Use() { } //This is a passive ability
}
