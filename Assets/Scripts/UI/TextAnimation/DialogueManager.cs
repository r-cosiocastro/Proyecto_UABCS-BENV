using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    /*
    private VoiceManager vm;
    public static DialogueManager instance;
    TextMeshProUGUI textBox;


    [TextArea]
    public string dialogue1;

    public bool animatingText = false;

    private DialogueVertexAnimator dialogueVertexAnimator;

    private string lastTTS = "";
    private string lastDialogue = "";
    private float speedPerCharacter = 1f / 20f;
    void Awake() {
        textBox = GetComponent<TextMeshProUGUI>();
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox);

        if (!DialogueManager.instance) {
            DialogueManager.instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    void Start() {
        vm = VoiceManager.instance;
    }

    public void RepeatLastDialogue() {
        //vm.Speak(lastTTS);
        PlayDialogue(DialogFormatPunctuation(lastDialogue));
    }

    public void PlayDialogueText(string textToSpeak, string dialogue, Action onFinished = null, float delayEnd = 0f) {
        lastDialogue = dialogue;
        lastTTS = textToSpeak;
        Speak(textToSpeak, onFinished, delayEnd);
        PlayDialogue(DialogFormatPunctuation(dialogue));
    }

    public void PlayDialogueText(string textToSpeak, Action onFinished = null, float delayEnd = 0f) {
        lastDialogue = textToSpeak;
        lastTTS = textToSpeak;
        Speak(textToSpeak, onFinished, delayEnd);
        PlayDialogue(DialogFormatPunctuation(textToSpeak));
    }

    private Coroutine typeRoutine = null;
    void PlayDialogue(string message, Action onFinished = null) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        animatingText = true;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, speedPerCharacter, onFinished));
    }

    void FinishAnimatingText() {
        animatingText = false;
    }

    string DialogFormatPunctuation(string originalText) {
        originalText = originalText.Replace(".", ".<p:long>");
        originalText = originalText.Replace(",", ",<p:short>");
        return originalText;
    }

    // TTS
    List<Dialog> pendingDialogList;
    /*
    public void Speak(string textToSpeak, Action onFinish = null, float delayEnd = 0f) {
        Dialog dialog = new Dialog(textToSpeak, onFinish, delayEnd);
        pendingDialogList.Add(dialog);

        if (pendingDialogList.Count == 1) {
            Speak(dialog);
        }
    }

    public void Speak(Dialog dialog) {
        Debug.Log("Text to Speak: " + dialog.textToSpeak);
        FinishSpeaking(dialog.actionOnFinish, dialog.delayToFinish);
        //vm.Speak(dialog.textToSpeak);
    }

    private Coroutine typeRoutine2 = null;
    void FinishSpeaking(Action onFinish, float delayEnd) {
        this.EnsureCoroutineStopped(ref typeRoutine2);
        typeRoutine2 = StartCoroutine(FinishSpeakingCallback(onFinish, delayEnd));
    }

    IEnumerator FinishSpeakingCallback(Action onFinish, float delayEnd) {
        yield return new WaitForSeconds(0.1f);
        while (vm.Status(0) == 2) {
            yield return null;
        }
        yield return new WaitForSeconds(delayEnd);
        onFinish?.Invoke();

        pendingDialogList.RemoveAt(0);
        if (pendingDialogList.Count > 0) {
            Speak(pendingDialogList[0]);
        }
    }

    */
}
