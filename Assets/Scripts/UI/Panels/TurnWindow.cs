using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnWindow : MonoBehaviour {
    private TextMeshProUGUI turnText;
    private TurnSystem turnSystem;
    private CanvasGroup canvasGroup;
    private void Awake() {
        turnText = transform.Find("TurnInformationTextPanel").Find("CurrentTurnText").GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetTurnSystem(TurnSystem turnSystem) {
        this.turnSystem = turnSystem;

        //Events
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
        turnSystem.OnIntro += TurnSystem_OnIntro;
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e) {
        GetComponent<Animator>().Play("Change");
        turnText.text = turnSystem.GetCurrentTurnString();
    }

    private void TurnSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.alpha = 1f;
        GetComponent<Animator>().Play("Intro");
        turnText.text = turnSystem.GetCurrentTurnString();
    }
}
