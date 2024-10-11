using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewParty : MonoBehaviour
{
    public GameObject menuPrefab;
    private GameObject partyMenu;
    private bool isMenuOpen;

    public void OpenMenu()
    {
        if (isMenuOpen == false)
        {
            partyMenu = Instantiate(menuPrefab, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
            partyMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(-28, -2.5f, 0);
            partyMenu.transform.localScale = new Vector3(4, 2, 1.0f);
            partyMenu.GetComponent<PartyMenu>().Display(GameObject.Find("PlayerParty").GetComponent<PartyManager>().party);
            Destroy(partyMenu.GetComponent<PartyMenu>().InventoryObj);
            partyMenu.GetComponent<PartyMenu>().equippedButtons[0].SetActive(false);
            partyMenu.GetComponent<PartyMenu>().equippedButtons[1].SetActive(false);
            isMenuOpen = true;
        }
    }
}
