using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { ARMOR = 0, WEAPON = 1, ARTIFACT = 2 }

public class ItemInterface : MonoBehaviour
{
    public ItemType itemType;
    public string itemName, description;
    public Sprite image;
    public int[] statBoosts;
    public GameObject inventoryItem; //button created when viewing party menu
    public GameObject equippedBy;
}
