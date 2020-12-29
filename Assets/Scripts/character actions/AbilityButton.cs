using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    float manaCost;
    GameObject caster, ability;

    public void SetReference(GameObject _caster, GameObject _ability)
    {
        ability = _ability;
        manaCost = ability.GetComponent<AbilityInterface>().manaCost;
        caster = _caster;
    }

    private void Update()
    {
        if (ability.GetComponent<AbilityInterface>().selected)
        {
            //selected
            GetComponent<Image>().color = Color.yellow;
        }
        else if (caster.GetComponent<PlayerStats>().currentMana >= manaCost)
        {
            //can cast
            GetComponent<Image>().color = Color.white;
        }
        else
        {
            //not enough mana
            GetComponent<Image>().color = Color.blue;
        }

    }

}
