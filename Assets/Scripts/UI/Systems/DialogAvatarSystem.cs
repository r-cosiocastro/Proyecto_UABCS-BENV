using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogAvatarSystem {
    public event EventHandler OnIntro;

    public DialogAvatarSystem() {

    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }
}
