using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Database;

public class StripSystem {
    public event EventHandler OnChangedTurn;
    private StudentEntity deleteNextTurn;
    private StudentEntity previousPlayer;
    private StudentEntity currentPlayer;
    private StudentEntity nextPlayer;
    private TurnSystem turnSystem;
    public PlayerSystem playerSystem;

    public StripSystem(TurnSystem turnSystem) {
        this.turnSystem = turnSystem;

        deleteNextTurn = new StudentEntity();
        previousPlayer = new StudentEntity();
    }

    public StudentEntity GetCurrentPlayer() {
        return currentPlayer;
    }

    public StudentEntity GetNextPlayer() {
        return nextPlayer;
    }

    public StudentEntity GetPlayerToDelete() {
        return deleteNextTurn;
    }

    public StudentEntity GetPreviousPlayer() {
        return previousPlayer;
    }

    public int GetCurrentTurn() {
        return turnSystem.GetCurrentTurn();
    }

    public int GetCurrentRound() {
        return turnSystem.GetCurrentRound();
    }

    public int GetTotalTurns() {
        return turnSystem.GetTotalTurns();
    }

    public void OrderPlayers() {
        deleteNextTurn = previousPlayer;
        previousPlayer = currentPlayer;
        currentPlayer = nextPlayer;
    }

    public void ChangeTurn(ref List<StudentEntity> studentList, int currentIndex, int nextIndex) {
        this.currentPlayer = studentList[currentIndex];
        this.nextPlayer = studentList[nextIndex];

        OnChangedTurn?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeTurn(StudentEntity currentPlayer, StudentEntity nextPlayer) {
        this.currentPlayer = currentPlayer;
        this.nextPlayer = nextPlayer;

        OnChangedTurn?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeTurn(StudentEntity currentPlayer) {
        this.currentPlayer = currentPlayer;

        OnChangedTurn?.Invoke(this, EventArgs.Empty);
    }

    public void ChangeTurn() {
        OnChangedTurn?.Invoke(this, EventArgs.Empty);
    }

    public void AddStarCurrentPlayer() {
        //previousPlayer.UpdateStudent();
    }

    public void AddTrophyCurrentPlayer() {
        currentPlayer._stars++;
    }
}
