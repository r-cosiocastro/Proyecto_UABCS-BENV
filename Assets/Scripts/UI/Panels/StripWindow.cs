using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StripWindow : MonoBehaviour {
    // Statuses
    const int TO_BE_DELETED = 0;
    const int PREVIOUS = 1;
    const int CURRENT = 2;
    const int NEXT = 3;

    // Colors
    Color32 Blue1 = new Color32(41, 128, 185, 255);
    Color32 Blue2 = new Color32(0, 197, 243, 255);
    Color32 Green1 = new Color32(39, 174, 96, 255);
    Color32 Green2 = new Color32(52, 243, 56, 255);
    Color32 Yellow1 = new Color32(211, 84, 0, 255);
    Color32 Yellow2 = new Color32(233, 220, 67, 255);
    Color32 Gray = new Color32(107, 107, 107, 255);

    private GameObject[] playerPanel = new GameObject[4];
    private Image[] playerBackground = new Image[4];
    private TextMeshProUGUI[] playerNameText = new TextMeshProUGUI[4];
    private Image[] avatarImage = new Image[4];
    private TextMeshProUGUI[] trophyCountText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] starCountText = new TextMeshProUGUI[4];
    private TextMeshProUGUI[] statusText = new TextMeshProUGUI[4];
    private Image[] statusColor = new Image[4];
    private CanvasGroup[] canvasGroup = new CanvasGroup[4];
    private GameObject[] statusPanel = new GameObject[4];
    private CanvasGroup panelCanvasGroup;
    private Transform playerContainer;

    [SerializeField] AudioClip appear;
    [SerializeField] AudioClip swipe;
    [SerializeField] AudioClip zoomOut;
    [SerializeField] AudioClip zoomIn;
    [SerializeField] AudioClip vanish;

    private AudioSource audioSource;

    private Image blurImage;
    private Image stripBars;
    private StripSystem stripSystem;

    private void Awake() {
        for (int i = 0; i < 4; i++) {
            playerPanel[i] = transform.Find("PlayerContainer").Find(i.ToString()).gameObject;
            canvasGroup[i] = transform.Find("PlayerContainer").Find(i.ToString()).GetComponent<CanvasGroup>();
            playerBackground[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("Background").GetComponent<Image>();
            playerNameText[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("NameGroup").Find("PlayerName").GetComponent<TextMeshProUGUI>();
            avatarImage[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("AvatarGroup").Find("AvatarImageGroup").Find("AvatarMask").Find("AvatarImage").GetComponent<Image>();
            trophyCountText[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("AvatarGroup").Find("ScoreGroup").Find("TrophiesGroup").Find("TrophyCountText").GetComponent<TextMeshProUGUI>();
            starCountText[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("AvatarGroup").Find("ScoreGroup").Find("StarsGroup").Find("StarsCountText").GetComponent<TextMeshProUGUI>();
            statusText[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("CurrentTurnGroup").Find("DialogImage").Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
            statusColor[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("CurrentTurnGroup").Find("DialogImage").Find("Image").GetComponent<Image>();
            statusPanel[i] = transform.Find("PlayerContainer").Find(i.ToString()).Find("CurrentTurnGroup").gameObject;
        }
        blurImage = transform.Find("BlurBackground").GetComponent<Image>();
        stripBars = transform.Find("Background").GetComponent<Image>();
        playerContainer = transform.Find("PlayerContainer");
        audioSource = GameObject.FindGameObjectWithTag("SoundEffects").GetComponent<AudioSource>();

        panelCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetStripSystem(StripSystem stripSystem) {
        this.stripSystem = stripSystem;

        // Events
        stripSystem.OnChangedTurn += StripSystem_OnChangedTurn;
    }

    // Events
    private void StripSystem_OnChangedTurn(object sender, System.EventArgs e) {
        playerContainer.transform.localScale = new Vector3(1f, 1f, 1f);
        playerContainer.localPosition = new Vector3(632.1606f, 0, 0);
        playerPanel[PREVIOUS].transform.localScale = new Vector3(2.2f, 2.2f, 2.2f);
        playerPanel[CURRENT].transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);

        statusPanel[PREVIOUS].transform.localPosition = new Vector3(0, -512, 0);
        statusPanel[CURRENT].transform.localPosition = new Vector3(0, -512, 0);
        statusPanel[NEXT].transform.localPosition = new Vector3(0, -512, 0);

        stripBars.transform.localPosition = new Vector3(-1920, 0, 0);
        LeanTween.moveLocalX(stripBars.gameObject, 0f, 0.5f).setEaseInExpo();
        audioSource.PlayOneShot(swipe);

        LeanTween.alphaCanvas(panelCanvasGroup, 1f, 0.5f);
        blurImage.color = new Color(1, 1, 1, 0);
        LeanTween.color(blurImage.GetComponent<RectTransform>(), new Color(1, 1, 1, 1), 1f);

        // Player to delete:

        if (stripSystem.GetCurrentTurn() > 1) {
            canvasGroup[TO_BE_DELETED].alpha = 1f;
            playerNameText[TO_BE_DELETED].text = stripSystem.GetPlayerToDelete()._nickname;
            // TODO: avatarImage[TO_BE_DELETED] = stripSystem.GetPlayerToDelete()._avatar;
            trophyCountText[TO_BE_DELETED].text = stripSystem.GetPlayerToDelete()._trophies.ToString();
            starCountText[TO_BE_DELETED].text = stripSystem.GetPlayerToDelete()._stars.ToString();
            statusText[TO_BE_DELETED].text = "Turno anterior";
            playerBackground[TO_BE_DELETED].color = Blue1;
            statusColor[TO_BE_DELETED].color = Blue2;
        } else {
            canvasGroup[TO_BE_DELETED].alpha = 0f;
        }

        // Previous player:

        if (stripSystem.GetCurrentTurn() > 0) {
            canvasGroup[PREVIOUS].alpha = 1f;
            playerNameText[PREVIOUS].text = stripSystem.GetPreviousPlayer()._nickname;
            // TODO: avatarImage[PREVIOUS] = stripSystem.GetPreviousPlayer()._avatar;
            trophyCountText[PREVIOUS].text = stripSystem.GetPreviousPlayer()._trophies.ToString();
            starCountText[PREVIOUS].text = (stripSystem.GetPreviousPlayer()._stars).ToString();
            statusText[PREVIOUS].text = "Turno anterior";
            playerBackground[PREVIOUS].color = Green1;  // Turn to blue
            statusColor[PREVIOUS].color = Green2;
        } else {
            canvasGroup[PREVIOUS].alpha = 0f;
        }

        // Current player:

        if (stripSystem.GetCurrentTurn() < stripSystem.GetTotalTurns() - 2) {
            canvasGroup[CURRENT].alpha = 1f;
            playerNameText[CURRENT].text = stripSystem.GetCurrentPlayer()._nickname;
            // TODO: avatarImage[CURRENT] = stripSystem.GetCurrentPlayer()._avatar;
            trophyCountText[CURRENT].text = stripSystem.GetCurrentPlayer()._trophies.ToString();
            starCountText[CURRENT].text = stripSystem.GetCurrentPlayer()._stars.ToString();
            statusText[CURRENT].text = "Turno actual";
            playerBackground[CURRENT].color = Yellow1;
            statusColor[CURRENT].color = Yellow2;
        } else {
            canvasGroup[CURRENT].alpha = 0f;
        }

        // Next player:

        if (stripSystem.GetCurrentTurn() < stripSystem.GetTotalTurns() - 2) {
            canvasGroup[NEXT].alpha = 1f;
            playerNameText[NEXT].text = stripSystem.GetNextPlayer()._nickname;
            // TODO: avatarImage[NEXT] = stripSystem.GetNextPlayer()._avatar;
            trophyCountText[NEXT].text = stripSystem.GetNextPlayer()._trophies.ToString();
            starCountText[NEXT].text = stripSystem.GetNextPlayer()._stars.ToString();
            statusText[NEXT].text = "Turno siguiente";
            playerBackground[NEXT].color = Gray;
            statusColor[NEXT].color = Yellow2;
        } else {
            canvasGroup[NEXT].alpha = 0f;
        }

        LeanTween.moveLocalX(playerContainer.gameObject, -14f, 1f).setEaseInQuart();
        audioSource.PlayOneShot(appear);
        LeanTween.scale(playerPanel[PREVIOUS], new Vector3(1.8f, 1.8f, 1.8f), 1f).setEaseInExpo();

        FunctionTimer.Create(() => {
            LeanTween.scale(playerPanel[CURRENT], new Vector3(2.2f, 2.2f, 2.2f), 0.5f).setEaseInExpo();
            audioSource.PlayOneShot(zoomIn);
        }, 1f);
        LeanTween.color(playerBackground[CURRENT].GetComponent<RectTransform>(), Green1, 2f);
        LeanTween.color(playerBackground[PREVIOUS].GetComponent<RectTransform>(), Blue1, 2f);
        LeanTween.color(playerBackground[NEXT].GetComponent<RectTransform>(), Yellow1, 2f);

        LeanTween.color(statusColor[CURRENT].GetComponent<RectTransform>(), Green2, 2f);
        LeanTween.color(statusColor[PREVIOUS].GetComponent<RectTransform>(), Blue2, 2f);
        LeanTween.color(statusColor[NEXT].GetComponent<RectTransform>(), Yellow2, 2f);

        FunctionTimer.Create(() => LeanTween.moveLocalY(statusPanel[PREVIOUS], -121, 1.25f).setEaseInExpo(), 1f);
        FunctionTimer.Create(() => LeanTween.moveLocalY(statusPanel[CURRENT], -121, 0.75f).setEaseInExpo(), 2f);
        FunctionTimer.Create(() => LeanTween.moveLocalY(statusPanel[NEXT], -121, 0.50f).setEaseInExpo(), 3f);

        FunctionTimer.Create(() => {
            LeanTween.moveLocalX(stripBars.gameObject, 1920f, 1f).setEaseInExpo();
            LeanTween.color(blurImage.GetComponent<RectTransform>(), new Color(1, 1, 1, 0), 1f);
            LeanTween.alphaCanvas(panelCanvasGroup, 0f, 2f);
            LeanTween.scale(playerContainer.gameObject, new Vector3(3f, 3f, 3f), 2f).setEaseInExpo();
            audioSource.PlayOneShot(vanish);
        }, 8f);


        //blurImage.gameObject.SetActive(false);

        // After animations
        stripSystem.OrderPlayers();
    }
}
