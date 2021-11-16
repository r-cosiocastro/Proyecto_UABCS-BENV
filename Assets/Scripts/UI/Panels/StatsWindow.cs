using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsWindow : MonoBehaviour {
    private TextMeshProUGUI currentPlayerText;
    private TextMeshProUGUI correctAnswersPlayer;
    private TextMeshProUGUI incorrectAnswersPlayer;
    private TextMeshProUGUI correctAnswersGroup;
    private TextMeshProUGUI incorrectAnswersGroup;
    private CanvasGroup canvasGroup;
    private StatsSystem statsSystem;

    private void Awake() {
        currentPlayerText = transform.Find("StatsInfoPanel")
        .Find("IndividualInfo")
        .Find("IndividualTitle")
        .Find("IndividualText").GetComponent<TextMeshProUGUI>();

        correctAnswersPlayer = transform.Find("StatsInfoPanel")
        .Find("IndividualInfo")
        .Find("IndividualNumbers")
        .Find("SuccessIndividual")
        .Find("SuccessIndividualText").GetComponent<TextMeshProUGUI>();

        incorrectAnswersPlayer = transform.Find("StatsInfoPanel")
        .Find("IndividualInfo")
        .Find("IndividualNumbers")
        .Find("FailIndividual")
        .Find("FailIndividualText").GetComponent<TextMeshProUGUI>();

        correctAnswersGroup = transform.Find("StatsInfoPanel")
        .Find("GroupInfo")
        .Find("GroupNumbers")
        .Find("SuccessGroup")
        .Find("SuccessGroupText").GetComponent<TextMeshProUGUI>();

        incorrectAnswersGroup = transform.Find("StatsInfoPanel")
        .Find("GroupInfo")
        .Find("GroupNumbers")
        .Find("FailGroup")
        .Find("FailGroupText").GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetStatsSystem(StatsSystem statsSystem) {
        this.statsSystem = statsSystem;

        // Events
        statsSystem.OnChangedData += StatsSystem_OnChangedData;
        statsSystem.OnIntro += StatsSystem_OnIntro;
    }

    private void StatsSystem_OnChangedData(object sender, System.EventArgs e) {
        currentPlayerText.text = statsSystem.GetCurrentPlayerName();
        correctAnswersPlayer.text = statsSystem.GetCorrectAnswersUser().ToString();
        incorrectAnswersPlayer.text = statsSystem.GetIncorrectAnswersUser().ToString();
        correctAnswersGroup.text = statsSystem.GetCorrectAnswersGroup().ToString();
        incorrectAnswersGroup.text = statsSystem.GetIncorrectAnswersGroup().ToString();
    }

    private void StatsSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Intro");
    }

    private void StatsSystem_OnCorrectAnswer() {

    }
}
