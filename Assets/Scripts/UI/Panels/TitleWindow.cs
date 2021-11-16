using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TitleWindow : MonoBehaviour {
    private TextMeshProUGUI titleText;
    private CanvasGroup canvasGroup;
    private TitleSystem titleSystem;

    private void Awake() {
        titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetTitleSystem(TitleSystem titleSystem) {
        this.titleSystem = titleSystem;

        //Events
        titleSystem.OnIntro += TitleSystem_OnIntro;
    }

    // Events

    private void TitleSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Show");
    }
}
