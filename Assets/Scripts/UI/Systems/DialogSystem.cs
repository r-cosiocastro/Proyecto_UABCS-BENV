using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogSystem {
    public event EventHandler OnIntro;
    public event EventHandler OnTextChanged;

    private VoiceManager vm;
    private string lastTTS = "";
    private string lastDialogue = "";
    List<Dialog> pendingDialogList;

    void Awake() {

    }

    public DialogSystem() {
        vm = VoiceManager.instance;
        pendingDialogList = new List<Dialog>();
    }

    public string GetTTSText() {
        return lastTTS;
    }

    public string GetDialogText() {
        return lastDialogue;
    }

    public void RepeatLastDialogue() {
        vm.Speak(lastTTS);
        OnTextChanged?.Invoke(this, EventArgs.Empty);
    }

    public void PlayDialogueText(string textToSpeak, string dialogue, Action onFinished = null, float delayEnd = 0f) {
        lastDialogue = dialogue;
        lastTTS = textToSpeak;
        Speak(textToSpeak, onFinished, delayEnd);
    }

    public void PlayDialogueText(string textToSpeak, string dialogue, float delayEnd = 0f) {
        lastDialogue = dialogue;
        lastTTS = textToSpeak;
        Speak(textToSpeak, null, delayEnd);
    }

    public void PlayDialogueText(string textToSpeak, string dialogue) {
        lastDialogue = dialogue;
        lastTTS = textToSpeak;
        Speak(textToSpeak, null, 0f);
    }

    // TTS

    public void Speak(string textToSpeak, Action onFinish = null, float delayEnd = 0f) {
        Dialog dialog = new Dialog(textToSpeak, SpeakNext, onFinish, delayEnd);
        pendingDialogList.Add(dialog);

        if (pendingDialogList.Count == 1) {
            SpeakNow(dialog);
        }
    }

    public void SpeakNow(Dialog dialog) {
        vm.Speak(dialog.textToSpeak, dialog.speakNextText, dialog.actionOnFinish, dialog.delayToFinish);
        OnTextChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SpeakNext() {
        pendingDialogList.RemoveAt(0);
        if (pendingDialogList.Count > 0)
            SpeakNow(pendingDialogList[0]);
    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }
}

public class Dialog {
    public string textToSpeak;
    public Action speakNextText;
    public Action actionOnFinish;
    public float delayToFinish;

    public Dialog(string text, Action speakNextText, Action onFinish = null, float delay = 0f) {
        textToSpeak = text;
        actionOnFinish = onFinish;
        delayToFinish = delay;
        this.speakNextText = speakNextText;
    }
}
