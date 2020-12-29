using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class DisplayStats : MonoBehaviour
{
    GameObject playerReference; //regerence to the player this is displaying

    public bool ShowModButtons;
    public GameObject modButtons; //+ and - buttons to edit stats
    
    int currentStat;    //which stat the mod buttons are currently on
    public int modPointsLeft;  //how many points can be put into modding a characters stats
    public int[] modChanges;   //tracks which mods have been modified

    // Start is called before the first frame update
    void Start()
    {
        //modButtons = GameObject.Find("ButtonModGroup");
        modChanges = new int[7];
        ShowModButtons = false;
        HideMods();
    }

    public void HideMods()
    {
        //ShowModButtons = false;
        modButtons.SetActive(false);
    }

    public void ShowMods()
    {
        ShowModButtons = true;
        modButtons.SetActive(true);
    }

    public void SetStats(GameObject player)
    {
        //get highest stat for sliders
        playerReference = player;
        PlayerStats statScript = player.GetComponent<PlayerStats>();
        float maxStat = statScript.maxHealth;
        if(statScript.maxMana > maxStat)
            maxStat = statScript.maxMana;
        if (statScript.attack > maxStat)
            maxStat = statScript.attack;
        if (statScript.magic > maxStat)
            maxStat = statScript.magic;
        if (statScript.defense > maxStat)
            maxStat = statScript.defense;
        if (statScript.resilience > maxStat)
            maxStat = statScript.resilience;
        if (statScript.speed > maxStat)
            maxStat = statScript.speed;

        Slider[] sliders = GetComponentsInChildren<Slider>();
        Text[] numberTexts = GetComponentsInChildren<Text>();

        //set text and slider labels
        sliders[1].maxValue = maxStat;
        sliders[1].value = statScript.maxHealth;
        numberTexts[0].text = statScript.maxHealth.ToString();
        sliders[0].maxValue = maxStat;
        sliders[0].value = statScript.maxMana;
        numberTexts[1].text = statScript.maxMana.ToString();
        sliders[2].maxValue = maxStat;
        sliders[2].value = statScript.attack;
        numberTexts[2].text = statScript.attack.ToString();
        sliders[3].maxValue = maxStat;
        sliders[3].value = statScript.magic;
        numberTexts[3].text = statScript.magic.ToString();
        sliders[4].maxValue = maxStat;
        sliders[4].value = statScript.defense;
        numberTexts[4].text = statScript.defense.ToString();
        sliders[5].maxValue = maxStat;
        sliders[5].value = statScript.resilience;
        numberTexts[5].text = statScript.resilience.ToString();
        sliders[6].maxValue = maxStat;
        sliders[6].value = statScript.speed;
        numberTexts[6].text = statScript.speed.ToString();

        if (ShowModButtons)
            numberTexts[17].text = "Upgrade Points: " + modPointsLeft;
        else
            numberTexts[17].text = "";
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //if showModButtons is true, set the buttons to the corerct position
        if (ShowModButtons)
        {
            //Debug.Log("Mouse Pos: " + mousePos.x + "," + mousePos.y);
            if (Mathf.Abs(mousePos.x - .6f - transform.position.x) < 2.1)
            {
                //find nearest slider to set y position
                Slider[] sliders = GetComponentsInChildren<Slider>();
                float yPos = -999;
                for(int i = 0; i < sliders.Length; i++)
                {
                    if (Mathf.Abs((mousePos.y + -.1f) - sliders[i].transform.position.y) < .275)
                    {
                        yPos = sliders[i].transform.position.y;
                        currentStat = i;
                    }
                }
                if (yPos == -999)
                    HideMods();
                //show buttons and set their position
                ShowMods();
                modButtons.transform.position = new Vector3(transform.position.x, yPos, 0);
            }
            else
            {
                HideMods();
            }
        }
    }

    //when + mod button is clicked
    public void AddToStat()
    {
        PlayerStats stats = playerReference.GetComponent<PlayerStats>();
        if (modPointsLeft > 0)
        {
            if (currentStat == 0)
            {
                stats.maxMana += 2;
                modChanges[0]++;
            }
            if (currentStat == 1)
            {
                stats.maxHealth += 2;
                modChanges[1]++;
            }
            if (currentStat == 2)
            {
                stats.attack += 1;
                modChanges[2]++;
            }
            if (currentStat == 3)
            {
                stats.magic += 1;
                modChanges[3]++;
            }
            if (currentStat == 4)
            {
                stats.defense += 1;
                modChanges[4]++;
            }
            if (currentStat == 5)
            {
                stats.resilience += 1;
                modChanges[5]++;
            }
            if (currentStat == 6)
            {
                stats.speed += 2;
                modChanges[6]++;
            }
            modPointsLeft--;
        }
        SetStats(playerReference);
    }

    //when - mod button is clicked
    public void SubtractFromStat()
    {
        modPointsLeft++;
        PlayerStats stats = playerReference.GetComponent<PlayerStats>();
        if (currentStat == 0 && modChanges[0] > 0)
        {
            stats.maxMana -= 2;
            modChanges[0]--;
        }
        else if (currentStat == 1 && modChanges[1] > 0)
        {
            stats.maxHealth -= 2;
            modChanges[1]--;
        }
        else if (currentStat == 2 && modChanges[2] > 0)
        {
            stats.attack -= 1;
            modChanges[2]--;
        }
        else if (currentStat == 3 && modChanges[3] > 0)
        {
            stats.magic -= 1;
            modChanges[3]--;
        }
        else if (currentStat == 4 && modChanges[4] > 0)
        {
            stats.defense -= 1;
            modChanges[4]--;
        }
        else if (currentStat == 5 && modChanges[5] > 0)
        {
            stats.resilience -= 1;
            modChanges[5]--;
        }
        else if (currentStat == 6 && modChanges[6] > 0)
        {
            stats.speed -= 2;
            modChanges[6]--;
        }
        else
        {
            modPointsLeft--;
        }
        SetStats(playerReference);
    }




}
