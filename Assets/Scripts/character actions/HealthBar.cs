using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float MaxValue = 1, CurrentValue = 1;

    void Update()
    {
        transform.localScale = new Vector3(Mathf.Max(CurrentValue,0) / MaxValue, 1, 1);
    }
}
