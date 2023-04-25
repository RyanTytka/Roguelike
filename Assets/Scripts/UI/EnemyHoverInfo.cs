using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHoverInfo : MonoBehaviour
{
    public Text nameText, descriptionText, healthText, atkText, magicText, defText, resilienceText, spdText;

    public void SetInfo(GameObject unit)
    {
        UnitStats stats = unit.GetComponent<UnitStats>();
        nameText.text = stats.unitName;
        atkText.text = stats.Attack + " Attack";
        magicText.text = stats.Magic + " Magic";
        defText.text = stats.Defense + " Defense";
        resilienceText.text = stats.Resilience + " Resilience";
        spdText.text = stats.Speed + " Speed";
    }
}
