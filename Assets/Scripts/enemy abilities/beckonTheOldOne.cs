using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beckonTheOldOne : AbilityInterface
{
    public override void Use()
    {
        CreateStatusEffect(StatusTypeEnum.DOOM, 1, caster);
    }

    public override string GetDescription()
    {
        return "Call on the Old One to bring doom to your enemies. Apply a stack of Doom to yourself.";
    }
}
