using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using TMPro;

public class PlayerWindow : MonoBehaviour
{
    private TextMeshProUGUI playerNameText;
    private Image avatarImage;
    private TextMeshProUGUI trophyCountText;
    private TextMeshProUGUI starCountText;
    private CanvasGroup canvasGroup;
    private PlayerSystem playerSystem;
    private void Awake() {
        playerNameText = transform.Find("CurrentPlayerWindow").Find("NameGroup").Find("CurrentPlayerName").GetComponent<TextMeshProUGUI>();
        avatarImage = transform.Find("CurrentPlayerWindow").Find("AvatarGroup").Find("AvatarImageGroup").Find("AvatarMask").Find("AvatarImage").GetComponent<Image>();
        trophyCountText = transform.Find("CurrentPlayerWindow").Find("AvatarGroup").Find("ScoreGroup").Find("TrophiesGroup").Find("TrophyCountText").GetComponent<TextMeshProUGUI>();
        starCountText = transform.Find("CurrentPlayerWindow").Find("AvatarGroup").Find("ScoreGroup").Find("StarsGroup").Find("StarsCountText").GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetPlayerSystem(PlayerSystem playerSystem){
        this.playerSystem = playerSystem;

        // Events
        playerSystem.OnIntro += PlayerSystem_OnIntro;
        playerSystem.OnChangedPlayerInfo += PlayerSystem_OnChangedPlayerInfo;
    }

    private void SetPlayerName(string playerName){
        playerNameText.text = playerName;
    }
    private void SetPlayerStars(int stars){
        starCountText.text = stars.ToString();
    }
    private void SetPlayerTrophies(int trophies){
        trophyCountText.text = trophies.ToString();
    }

    private void SetPlayerInfo(){
        playerNameText.text = playerSystem.GetName();
        starCountText.text = playerSystem.GetStars().ToString();
        trophyCountText.text = playerSystem.GetTrophies().ToString();
    }

    private void PlayerSystem_OnIntro(object sender, System.EventArgs e){
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Show");
    }

    private void PlayerSystem_OnChangedPlayerInfo(object sender, System.EventArgs e){
        GetComponent<Animator>().Play("Hide");
        FunctionTimer.Create(SetPlayerInfo, 1f);
        GetComponent<Animator>().Play("Show");
    }
}
