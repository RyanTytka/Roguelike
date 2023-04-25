using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHistory : MonoBehaviour
{
    private Queue<string> log = new Queue<string>();
    public GameObject logDisplay;
    public Text textObj;

    public void AddLog(string s)
    {
        log.Enqueue(s);
        if (log.Count > 3)
            log.Dequeue();

        // Update UI
        string result = "";
        for(int i = 0; i < log.Count; i++)
            result += log.ToArray()[i] + "\n";
        textObj.text = result;
    }

    //display info
    void OnMouseOver()
    {
        logDisplay.SetActive(true);
    }

    //hide info
    void OnMouseExit()
    {
        logDisplay.SetActive(false);
    }
}
