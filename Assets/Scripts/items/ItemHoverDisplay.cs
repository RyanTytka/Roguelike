using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHoverDisplay : MonoBehaviour
{
    public GameObject infoDisplay, currentInfoDisplay, itemObj;
    
    //Which kind of item select this is 
    public int type; 
    //0 = Inventory Item
    //1 = New Item Select
    //3 = 

    public void OnMouseEnter()
    {
        currentInfoDisplay = Instantiate(infoDisplay, GameObject.Find("Canvas").transform);
        currentInfoDisplay.transform.position = new Vector3(transform.position.x, transform.position.y + 1, 0);

        currentInfoDisplay.GetComponentInChildren<Text>().text = itemObj.GetComponent<ItemInterface>().description;
    }

    public void OnMouseExit()
    {
        Destroy(currentInfoDisplay);
    }



}
