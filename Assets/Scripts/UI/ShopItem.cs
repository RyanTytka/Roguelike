using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image displayImage;
    public Text nameText, costText;
    public GameObject itemRef;

    public int cost;

    private bool sold = false;

    private void Update()
    {
        if(sold)
        {
            costText.text = "Sold Out";
            displayImage.color = Color.black;
        }
        else if(cost <= GameObject.Find("PlayerParty").GetComponent<PartyManager>().Gold)
        {
            //can buy
            displayImage.color = Color.white;
            
        }
        else
        {
            //cant buy
            displayImage.color = Color.gray;

        }
    }

    public void Buy()
    {
        if(!sold && cost <= GameObject.Find("PlayerParty").GetComponent<PartyManager>().Gold)
        {
            GameObject itemMan = GameObject.Find("ItemManager");
            itemMan.GetComponent<ItemManager>().inventory.Add(itemRef);
            itemRef.transform.SetParent(itemMan.transform);
            sold = true;
            GameObject.Find("PlayerParty").GetComponent<PartyManager>().Gold -= cost;
        }
    }
}
