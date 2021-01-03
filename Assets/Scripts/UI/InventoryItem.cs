using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public GameObject equippedByImage, selectedIcon;
    public GameObject itemReference;
    public GameObject equippedBy; //who is using this item
    public bool selected;   //if this item is selected or not

    private void Update()
    {
        equippedByImage.SetActive(equippedBy != null);
        if(equippedBy != null)
        {
            equippedByImage.GetComponent<Image>().sprite = equippedBy.GetComponent<SpriteRenderer>().sprite;
        }
        selectedIcon.SetActive(selected);
    }
}
