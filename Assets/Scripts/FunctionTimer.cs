using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FunctionTimer
{
    private static List<FunctionTimer> activeTimerList;
    private static GameObject initGameObject;

    private static void InitIfNeeded(){
        if(initGameObject == null){
            initGameObject = new GameObject("FunctionTimer_InitGameObject");
            activeTimerList = new List<FunctionTimer>();
        }
    }

    public static FunctionTimer Create(Action action, float timer, string timerName = null){
        InitIfNeeded();
        GameObject gameObject = new GameObject("FunctionTimer", typeof(MonoBehaviourHook));
        FunctionTimer functionTimer = new FunctionTimer(action, timer, timerName, gameObject);

        gameObject.GetComponent<MonoBehaviourHook>().onUpdate = functionTimer.Update;
        activeTimerList.Add(functionTimer);
        return functionTimer;
    }

    private static void RemoveTimer(FunctionTimer functionTimer){
        InitIfNeeded();
        activeTimerList.Remove(functionTimer);
    }

    public static void StopTimer(string timerName){
        for(int i=0; i<activeTimerList.Count; i++){
            if(activeTimerList[i].timerName == timerName){
                // Stop this timer
                activeTimerList[i].DestroySelf();
                i--;
            }
        }
    }

    public class MonoBehaviourHook : MonoBehaviour{
        public Action onUpdate;
        private void Update(){
            if(onUpdate != null) onUpdate();
        }
    }
    private Action action;
    private float timer;
    private string timerName;
    private GameObject gameObject;
    private bool isDestroyed;
    private FunctionTimer(Action action, float timer, string timerName, GameObject gameObject){
        this.action = action;
        this.timer = timer;
        this.timerName = timerName;
        this.gameObject = gameObject;
        isDestroyed = false;
    }

    public void Update(){
        if(!isDestroyed){
            timer -= Time.deltaTime;
            if(timer < 0){
                // Trigger action
                action?.Invoke();
                DestroySelf();
            }
        }
    }

    private void DestroySelf(){
        isDestroyed = true;
        UnityEngine.Object.Destroy(gameObject);
        RemoveTimer(this);
    }
}
