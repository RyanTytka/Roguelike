using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    private float timer = 0;
    private bool fading = false;

    public void Init(string preface, float damage, Color color, Vector3 pos)
    {
        GetComponent<RectTransform>().position = pos;
        GetComponent<Text>().text = preface + Mathf.Round(damage * 10f) / 10f;
        GetComponent<Text>().color = color;
    }

    void Update()
    {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(0, Time.deltaTime / 2, 0));
        if (timer >= 2 && fading == false)
        {
            GetComponent<Text>().CrossFadeAlpha(0, 1, false);
            fading = true;
        }
        if (timer > 3)
        {
            Destroy(this.gameObject);
        }
    }
}
