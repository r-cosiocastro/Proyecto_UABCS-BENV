using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TimeWindow : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private Transform clockHandTransform;
    private float currentTime;
    private bool isActive;
    private CanvasGroup canvasGroup;
    private TimeSystem timeSystem;
    private float day;

    private void Awake() {
        timerText = transform.Find("TimeText").GetComponent<TextMeshProUGUI>();
        clockHandTransform = transform.Find("Icon").Find("ClockHand");
        currentTime = 0f;
        isActive = false;

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    void Update()
    {
        if(isActive){
            currentTime = currentTime + Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = time.ToString(@"mm\:ss");

            float secondsNormalized = (currentTime/60) % 1f;

            clockHandTransform.eulerAngles = new Vector3(0,0, -secondsNormalized * 360f);
        }
    }

    public void SetTimeSystem(TimeSystem timeSystem){
        this.timeSystem = timeSystem;

        timerText.text = "00:00";

        timeSystem.OnIntro += TimeSystem_OnIntro;
        timeSystem.OnTimerStarted += TimeSystem_OnStart;
        timeSystem.OnTimerPaused += TimeSystem_OnPause;
    }

    private void StartResumeTimer(){
        isActive = true;
    }

    private void PauseTimer(){
        isActive = false;
    }

    private void TimeSystem_OnIntro(object sender, System.EventArgs e){
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Intro");
    }

    private void TimeSystem_OnPause(object sender, System.EventArgs e){
        PauseTimer();
    }

    private void TimeSystem_OnStart(object sender, System.EventArgs e){
        StartResumeTimer();
        GetComponent<Animator>().Play("StartTimer");
        Debug.Log("timer started, presumably");
    }
}
