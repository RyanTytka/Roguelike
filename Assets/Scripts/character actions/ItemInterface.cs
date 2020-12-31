using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInterface : MonoBehaviour
{
    public string itemName, description;
    public Sprite image;
    public int[] statBoosts;
}
