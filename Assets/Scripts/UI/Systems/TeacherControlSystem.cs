using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeacherControlSystem {
    public event EventHandler OnIntro;
    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }

    public TeacherControlSystem() {

    }
}