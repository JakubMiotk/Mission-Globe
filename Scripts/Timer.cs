using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;   
    public bool countDown;
    public bool hasLimit;
    public float timerLimit;
    public string currentTime;
    public float currentSeconds;
    public float currentMinutes;
    void Start()
    {
        currentMinutes = 0f;
        currentSeconds = 0f;
    }
    void FixedUpdate()
    {
        if (currentSeconds < 60f)
            currentSeconds = countDown ? currentSeconds -= Time.deltaTime : currentSeconds += Time.deltaTime;
        else
        {
            currentMinutes = currentMinutes + 1;
            currentSeconds = 0;
        }

        if(hasLimit && ((countDown && currentSeconds <= timerLimit) || (!countDown && currentSeconds >= timerLimit)))
        {
            currentSeconds = timerLimit;
            currentTime = string.Format("{0:00}:{1:00}", currentMinutes.ToString(), currentSeconds.ToString());
            timerText.text = currentTime;
            enabled = false;
        }
        currentTime = string.Format("{0:00}:{1:00}", currentMinutes.ToString("00"), currentSeconds.ToString("00"));
        timerText.text = currentTime;
    }

}
