using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectIcon : MonoBehaviour
{
    //this script displays the info of a status effect when the icon is moused over

    public StatusEffect statusEffect;
    public GameObject hoverDisplayPrefab;
    public GameObject activeHoverDisplay;

    void OnMouseOver()
    {
        if (activeHoverDisplay == null)
        {
            activeHoverDisplay = Instantiate(hoverDisplayPrefab, GameObject.Find("HUDCanvas").transform);
            activeHoverDisplay.transform.position = new Vector3(-2, 0, 0);
            activeHoverDisplay.GetComponent<StatusEffectInfoDisplay>().SetInfo(statusEffect);
        }
    }

    void OnMouseExit()
    {
        Destroy(activeHoverDisplay);
    }
}
