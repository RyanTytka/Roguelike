using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAbilityDisplayInfo : MonoBehaviour
{
    //this class shows the ability info window during new character ability selection
    public GameObject ability;
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
}
