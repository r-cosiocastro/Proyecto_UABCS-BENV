using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnSystem {
    public event EventHandler OnTurnChanged;
    public event EventHandler OnIntro;
    private int currentTurn;
    private int totalTurns;
    private int totalRounds;
    private int currentRound;

    public TurnSystem(int totalTurns) {
        currentTurn = 0;
        currentRound = 1;
        this.totalTurns = totalTurns;
    }

    public TurnSystem() {
        currentTurn = 0;
        totalTurns = 0;
        currentRound = 1;
    }

    public int GetCurrentRound() {
        return currentRound;
    }

    public void SetTotalTurns(int totalTurns) {
        this.totalTurns = totalTurns;
    }

    public void SetTotalRounds(int totalRounds) {
        this.totalRounds = totalRounds;
    }

    public int GetCurrentTurn() {
        return currentTurn;
    }

    public void NextTurn() {
        currentTurn++;
        currentRound = (currentTurn / totalRounds) + 1;
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
