using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    float manaCost;
    GameObject caster, ability;

    public GameObject hoverDisplayPrefab;
    public GameObject activeHoverDisplay;

    void OnMouseOver()
    {
        if (activeHoverDisplay == null)
        {
            activeHoverDisplay = Instantiate(hoverDisplayPrefab, GameObject.Find("HUDCanvas").transform);
            activeHoverDisplay.transform.position = new Vector3(-2, 0, 0);
            activeHoverDisplay.GetComponent<AbilityInfo>().SetInfo(ability);
        }
    }

    void OnMouseExit()
    {
        Destroy(activeHoverDisplay);
    }

    public void SetReference(GameObject _caster, GameObject _ability)
    {
        ability = _ability;
        manaCost = ability.GetComponent<AbilityInterface>().manaCost;
        caster = _caster;
    }

    private void Update()
    {
        if (ability.GetComponent<AbilityInterface>() == null)
            return;

        if (ability.GetComponent<AbilityInterface>().selected)
        {
            //selected
            GetComponent<Image>().color = Color.yellow;
            //GetComponent<Button>().interactable = false;
        }
        else if (caster.GetComponent<PlayerStats>().currentMana >= manaCost)
        {
            //can cast
            GetComponent<Image>().color = Color.white;
            //GetComponent<Button>().interactable = true;
        }
        else
        {
            //not enough mana
            GetComponent<Image>().color = Color.blue;
            //GetComponent<Button>().interactable = false;
        }

    }

}
