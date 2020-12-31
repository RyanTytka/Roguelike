using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemSelect : MonoBehaviour
{
    public Image[] choiceImages;
    public GameObject choice1, choice2, choice3;

    void Start()
    {
        choiceImages = GetComponentsInChildren<Image>();

        choiceImages[0].sprite = choice1.GetComponent<ItemInterface>().image;
        choiceImages[1].sprite = choice2.GetComponent<ItemInterface>().image;
        choiceImages[2].sprite = choice3.GetComponent<ItemInterface>().image;
    }

    public void AddChoice(GameObject choice)
    {
        if (choice1 == null)
            choice1 = choice;
        else if (choice2 == null)
            choice2 = choice;
        else
            choice3 = choice;
    }


}
