using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DialogWindow : MonoBehaviour {
    private DialogueVertexAnimator dialogueVertexAnimator;
    private float speedPerCharacter = 1f / 20f;
    public bool animatingText = false;
    private TextMeshProUGUI dialogText;
    private CanvasGroup canvasGroup;
    private DialogSystem dialogSystem;
    private void Awake() {
        dialogText = transform.Find("DialogImage").Find("DialogText").GetComponent<TextMeshProUGUI>();

        dialogueVertexAnimator = new DialogueVertexAnimator(dialogText);

        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetDialogSystem(DialogSystem dialogSystem) {
        this.dialogSystem = dialogSystem;

        // Events
        dialogSystem.OnIntro += DialogSystem_OnIntro;
        dialogSystem.OnTextChanged += DialogSystem_OnTextChanged;
    }

    private void DialogSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.LeanAlpha(1f, 1f);
        GetComponent<Animator>().Play("Intro");
    }

    private void DialogSystem_OnTextChanged(object sender, System.EventArgs e) {
        PlayDialogue(DialogFormatPunctuation(dialogSystem.GetDialogText()));
    }

    private Coroutine typeRoutine = null;
    void PlayDialogue(string message) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        animatingText = true;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, speedPerCharacter));
    }

    void FinishAnimatingText() {
        animatingText = false;
    }

    string DialogFormatPunctuation(string originalText) {
        originalText = originalText.Replace(".", ".<p:long>");
        originalText = originalText.Replace(",", ",<p:short>");
        return originalText;
    }
}
