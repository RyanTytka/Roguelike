using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public GameObject armor;
    public GameObject weapon;
    public GameObject artifact;

    //sets appropriate equipment slot to item, then returns item that was in slot
    public GameObject EquipItem(GameObject item)
    {
        GameObject itemRef = item.GetComponent<InventoryItem>().itemReference;
        item.GetComponent<InventoryItem>().equippedBy = this.gameObject;
        if((int)itemRef.GetComponent<ItemInterface>().itemType == 0)
        {
            GameObject temp = armor;
            armor = itemRef;
            return temp;
        }
        else if((int)itemRef.GetComponent<ItemInterface>().itemType == 1)
        {
            GameObject temp = weapon;
            weapon = itemRef;
            return temp;
        }
        else
        {
            GameObject temp = artifact;
            artifact = itemRef;
            return temp;
        }
    }
}
