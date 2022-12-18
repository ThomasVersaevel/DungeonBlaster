using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text textTimer;
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;
        DisplayTime();
    }
    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60.0f);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

