using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherControlWindow : MonoBehaviour {
    private TeacherControlSystem teacherControlSystem;
    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }

    public void SetTeacherControlSystem(TeacherControlSystem teacherControlSystem) {
        this.teacherControlSystem = teacherControlSystem;

        // Events
        teacherControlSystem.OnIntro += TeacherControlSystem_OnIntro;
    }

    public void TeacherControlSystem_OnIntro(object sender, System.EventArgs e) {
        canvasGroup.alpha = 1f;
        GetComponent<Animator>().Play("Intro");
    }
}
