using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem : MonoBehaviour
{
    public event EventHandler OnTurnChanged;
    private int currentTurn;
    private int totalTurns;

    public TurnSystem(int totalTurns){
        currentTurn = 1;
        this.totalTurns = totalTurns;
    }

    public void SetTotalTurns(int totalTurns){
        this.totalTurns = totalTurns;
    }

    public string GetCurrentTurnString(){
        return string.Format("{0}/{1}", currentTurn, totalTurns);
    }
    public void ChangeTurn(int currentTurn){
        this.currentTurn = currentTurn;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    
}
