using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectInfoDisplay : MonoBehaviour
{
    public Text nameText, durationText, descriptionText;

    public void SetInfo(StatusEffect se)
    {
        nameText.text = se.statusName;
        string dt = se.stacks + " round";
        if (se.stacks > 1)
            dt += "s";
        durationText.text = dt;
        descriptionText.text = se.description;
    }
}
