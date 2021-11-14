using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogWindow : MonoBehaviour
{
    private TextMeshProUGUI dialogText;
    private CanvasGroup canvasGroup;
    private DialogSystem dialogSystem;
    private void Awake() {
        dialogText = transform.Find("DialogImage").Find("DialogText").GetComponent<TextMeshProUGUI>();
        
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetDialogSystem(DialogSystem dialogSystem){
        this.dialogSystem = dialogSystem;

        // Events
        dialogSystem.OnIntro += DialogSystem_OnIntro;
    }

    private void DialogSystem_OnIntro(object sender, System.EventArgs e){
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Intro");
    }
}
