using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using TMPro;

public class PlayerWindow : MonoBehaviour {
    private TextMeshProUGUI playerNameText;
    private Image avatarImage;
    private TextMeshProUGUI trophyCountText;
    private TextMeshProUGUI starCountText;
    private CanvasGroup canvasGroup;
    private PlayerSystem playerSystem;
    private void Awake() {
        playerNameText = transform.Find("PlayerWindow").Find("NameGroup").Find("PlayerName").GetComponent<TextMeshProUGUI>();
        avatarImage = transform.Find("PlayerWindow").Find("AvatarGroup").Find("AvatarImageGroup").Find("AvatarMask").Find("AvatarImage").GetComponent<Image>();
        trophyCountText = transform.Find("PlayerWindow").Find("AvatarGroup").Find("ScoreGroup").Find("TrophiesGroup").Find("TrophyCountText").GetComponent<TextMeshProUGUI>();
        starCountText = transform.Find("PlayerWindow").Find("AvatarGroup").Find("ScoreGroup").Find("StarsGroup").Find("StarsCountText").GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetPlayerSystem(PlayerSystem playerSystem) {
        this.playerSystem = playerSystem;

        // Events
        playerSystem.OnIntro += PlayerSystem_OnIntro;
        playerSystem.OnChangedPlayerInfo += PlayerSystem_OnChangedPlayerInfo;
        playerSystem.OnStarEarned += PlayerSystem_OnStarEarned;
    }

    private void SetPlayerInfo() {
        playerNameText.text = playerSystem.GetName();
        starCountText.text = playerSystem.GetStars().ToString();
        trophyCountText.text = playerSystem.GetTrophies().ToString();
    }

    // Events

    private void PlayerSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Show");
    }

    private void PlayerSystem_OnStarEarned(object sender, System.EventArgs e) {
        GetComponent<Animator>().SetTrigger("StarEarned");
        SetPlayerInfo();
    }

    private void PlayerSystem_OnChangedPlayerInfo(object sender, System.EventArgs e) {
        //GetComponent<Animator>().Play("Hide");
        FunctionTimer.Create(SetPlayerInfo, 1f);
        //FunctionTimer.Create(() => GetComponent<Animator>().Play("Show"), 2f);
    }
}
