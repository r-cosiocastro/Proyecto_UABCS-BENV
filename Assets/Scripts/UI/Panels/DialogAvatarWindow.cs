using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogAvatarWindow : MonoBehaviour
{
    private DialogAvatarSystem dialogAvatarSystem;
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f;
    }

    public void SetTeacherControlSystem(DialogAvatarSystem dialogAvatarSystem){
         this.dialogAvatarSystem = dialogAvatarSystem;

         // Events
         dialogAvatarSystem.OnIntro += DialogAvatarSystem_OnIntro;
    }

    public void DialogAvatarSystem_OnIntro(object sender, System.EventArgs e){
        canvasGroup.alpha = 1f;
        GetComponent<Animator>().Play("Intro");
    }
}
