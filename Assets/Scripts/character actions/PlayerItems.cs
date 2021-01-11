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
        itemRef.GetComponent<ItemInterface>().equippedBy = this.gameObject;
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

    //when a player unequips an item, use this to remove it from the slot
    public void UnequipItem(int type)
    {
        if (type == 0)
            armor = null;
        else if (type == 1)
            weapon = null;
        else
            artifact = null;
    }

    //adds all stat bonuses from equipped items
    public float[] StatMods()
    {
        float[] sums = new float[8];

        int i = 0;
        if (armor != null)
        {
            foreach (float f in armor.GetComponent<ItemInterface>().statBoosts)
            {
                sums[i] += f;
                i++;
            }
        }

        if (weapon != null)
        {
            i = 0;
            foreach (float f in weapon.GetComponent<ItemInterface>().statBoosts)
            {
                sums[i] += f;
                i++;
            }
        }

        if (artifact != null)
        {
            i = 0;
            foreach (float f in artifact.GetComponent<ItemInterface>().statBoosts)
            {
                sums[i] += f;
                i++;
            }
        }

        return sums;
    }
}
