using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerScript : MonoBehaviour
{
    [SerializeField] bool timerActive;
    float currentTime;
    TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
            currentTime = currentTime + Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timerText.text = time.ToString(@"mm\:ss");
    }
}
