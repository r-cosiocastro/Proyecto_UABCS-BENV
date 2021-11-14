using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeSystem
{
    public EventHandler OnIntro;
    public EventHandler OnTimerPaused;
    public EventHandler OnTimerStarted;

    private bool isActive;

    public TimeSystem(){
        isActive = false;
    }

    bool TimerIsActive(){
        return isActive;
    }

    public void ShowIntro(){
        OnIntro?.Invoke(this, EventArgs.Empty);
    }

    public void StartTimer(){
        OnTimerStarted?.Invoke(this, EventArgs.Empty);
    }

    public void StopTimer(){
        OnTimerPaused?.Invoke(this, EventArgs.Empty);
    }
}
