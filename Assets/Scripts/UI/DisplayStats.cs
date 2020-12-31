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

    public Slider[] sliders;
    public Text[] labels;

    // Start is called before the first frame update
    void Start()
    {
        //modButtons = GameObject.Find("ButtonModGroup");
        modChanges = new int[8];
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

        //set text and slider labels
        labels[0].text = statScript.maxHealth.ToString();
        sliders[0].maxValue = maxStat;
        sliders[0].value = statScript.maxHealth;

        labels[1].text = statScript.maxMana.ToString();
        sliders[1].maxValue = maxStat;
        sliders[1].value = statScript.maxMana;

        labels[2].text = statScript.manaRegen.ToString();
        sliders[2].maxValue = maxStat;
        sliders[2].value = statScript.manaRegen;

        labels[3].text = statScript.attack.ToString();
        sliders[3].maxValue = maxStat;
        sliders[3].value = statScript.attack;

        labels[4].text = statScript.magic.ToString();
        sliders[4].maxValue = maxStat;
        sliders[4].value = statScript.magic;

        labels[5].text = statScript.defense.ToString();
        sliders[5].maxValue = maxStat;
        sliders[5].value = statScript.defense;

        labels[6].text = statScript.resilience.ToString();
        sliders[6].maxValue = maxStat;
        sliders[6].value = statScript.resilience;

        labels[7].text = statScript.speed.ToString();
        sliders[7].maxValue = maxStat;
        sliders[7].value = statScript.speed;

        if (ShowModButtons)
            labels[8].text = "Upgrade Points: " + modPointsLeft;
        else
            labels[8].text = "";
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
                //Slider[] sliders = GetComponentsInChildren<Slider>();
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
                stats.maxHealth += 2;
            if (currentStat == 1)
                stats.maxMana += 2;
            if (currentStat == 2)
                stats.manaRegen += 0.5f;
            if (currentStat == 3)
                stats.attack += 1;
            if (currentStat == 4)
                stats.magic += 1;
            if (currentStat == 5)
                stats.defense += 1;
            if (currentStat == 6)
                stats.resilience += 1;
            if (currentStat == 7)
                stats.speed += 2;
            modChanges[currentStat]++;
            modPointsLeft--;
        }
        SetStats(playerReference);
    }

    //when - mod button is clicked
    public void SubtractFromStat()
    {
        if (modChanges[currentStat] == 0)
            return;

        modPointsLeft++;
        PlayerStats stats = playerReference.GetComponent<PlayerStats>();
        
        if (currentStat == 0)
            stats.maxHealth -= 2;
        if (currentStat == 1)
            stats.maxMana -= 2;
        if (currentStat == 2)
            stats.manaRegen -= 0.5f;
        if (currentStat == 3)
            stats.attack -= 1;
        if (currentStat == 4)
            stats.magic -= 1;
        if (currentStat == 5)
            stats.defense -= 1;
        if (currentStat == 6)
            stats.resilience -= 1;
        if (currentStat == 7)
            stats.speed -= 2;
        
        modChanges[currentStat]--;
        SetStats(playerReference);
    }




}
