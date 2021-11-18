using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Database;

public class PlayerSystem {
    public event EventHandler OnIntro;
    public event EventHandler OnStarEarned;
    public event EventHandler OnTrohpyEarned;
    public event EventHandler OnChangedPlayerInfo;
    private Image avatar;

    private List<StudentEntity> playerList;
    private int currentPlayerIndex = -1;
    private int nextPlayerIndex = -1;

    public PlayerSystem(List<StudentEntity> list) {
        //avatar.sprite = Resources.Load<Sprite>("Juanito");
        playerList = new List<StudentEntity>(list);
    }
    public PlayerSystem() {
        //avatar.sprite = Resources.Load<Sprite>("Juanito");
    }

    public string GetName() {
        return playerList[currentPlayerIndex]._nickname;
    }

    public int GetStars() {
        return playerList[currentPlayerIndex]._stars;
    }

    public int GetTrophies() {
        return playerList[currentPlayerIndex]._trophies;
    }

    public StudentEntity GetCurrentPlayer() {
        return playerList[currentPlayerIndex];
    }

    public StudentEntity GetNextPlayer() {
        return playerList[nextPlayerIndex];
    }

    public void AddTrohpy(int amount) {
        playerList[currentPlayerIndex].AddTrohpy(amount);

        OnTrohpyEarned?.Invoke(this, EventArgs.Empty);
    }
    public void AddStar(int amount) {
        playerList[currentPlayerIndex].AddStar(amount);

        Debug.Log("Star earned: " + playerList[currentPlayerIndex]._stars);

        OnStarEarned?.Invoke(this, EventArgs.Empty);
    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }

    public void NextPlayer() {
        currentPlayerIndex = currentPlayerIndex + 1 >= playerList.Count ? 0 : currentPlayerIndex + 1;
        nextPlayerIndex = currentPlayerIndex + 1 >= playerList.Count ? 0 : currentPlayerIndex + 1;

        OnChangedPlayerInfo?.Invoke(this, EventArgs.Empty);
    }


}
