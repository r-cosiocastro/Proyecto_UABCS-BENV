using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem {
    public event EventHandler OnTurnChanged;
    public event EventHandler OnIntro;
    private int currentTurn;
    private int totalTurns;

    public TurnSystem(int totalTurns) {
        currentTurn = 0;
        this.totalTurns = totalTurns;
    }

    public TurnSystem() {
        currentTurn = 0;
        totalTurns = 0;
    }

    public void SetTotalTurns(int totalTurns) {
        this.totalTurns = totalTurns;
    }

    public int GetCurrentTurn() {
        return currentTurn;
    }

    public void NextTurn() {
        currentTurn++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetTotalTurns() {
        return totalTurns;
    }

    public string GetCurrentTurnString() {
        return string.Format("{0}/{1}", currentTurn, totalTurns);
    }
    public void ChangeTurn(int currentTurn) {
        this.currentTurn = currentTurn;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }


}
