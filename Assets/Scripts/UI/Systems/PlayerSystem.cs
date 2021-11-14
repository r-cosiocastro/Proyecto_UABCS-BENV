using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Database;

public class PlayerSystem
{
    public event EventHandler OnIntro;
    public event EventHandler OnChangedPlayerInfo;
    private Image avatar;
    private string nickname;
    private int stars;
    private int trophies;
    private StudentEntity player;

    public PlayerSystem(){
        //avatar.sprite = Resources.Load<Sprite>("Juanito");
        nickname = "Dummy";
        stars = 0;
        trophies = 0;
    }

    public string GetName(){
        return player._nickname;
    }

    public void AddStar(int amount){
        stars += amount;
    }

    public void AddTrohpy(int amount){
        trophies += amount;
    }

    public int GetStars(){
        return player._stars;
    }

    public int GetTrophies(){
        return player._trophies;
    }

    public void ShowIntro(){
        OnIntro?.Invoke(this, EventArgs.Empty);
    }

    public void ChangePlayer(StudentEntity nextPlayer){
        player = nextPlayer;
        OnChangedPlayerInfo?.Invoke(this, EventArgs.Empty);
    }


}
