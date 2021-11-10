using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    TextMeshProUGUI textBox;


    [TextArea]
    public string dialogue1;

    private DialogueVertexAnimator dialogueVertexAnimator;
    private float speedPerCharacter = 1f / 20f;
    void Awake() {
        textBox = GetComponent<TextMeshProUGUI>();
        dialogueVertexAnimator = new DialogueVertexAnimator(textBox);
    }

    void Start(){
        //PlayDialogue(dialogue1);
    }

    public void PlayDialogueText(string dialogue) {
        PlayDialogue(dialogue);
    }


    private Coroutine typeRoutine = null;
    void PlayDialogue(string message) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        dialogueVertexAnimator.textAnimating = false;
        List<DialogueCommand> commands = DialogueUtility.ProcessInputString(message, out string totalTextMessage);
        typeRoutine = StartCoroutine(dialogueVertexAnimator.AnimateTextIn(commands, totalTextMessage, speedPerCharacter, null));
    }
}
