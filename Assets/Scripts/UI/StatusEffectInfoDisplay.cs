using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectInfoDisplay : MonoBehaviour
{
    public Text nameText, durationText, descriptionText;

    public void SetInfo(StatusEffect se)
    {
        nameText.text = se.statusName + " " + se.tierName;
        string dt = se.duration + " round";
        if (se.duration > 1)
            dt += "s";
        durationText.text = dt;
        descriptionText.text = se.description;
    }
}
