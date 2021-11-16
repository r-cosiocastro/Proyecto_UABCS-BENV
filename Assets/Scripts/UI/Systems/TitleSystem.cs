using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TitleSystem {
    public event EventHandler OnIntro;
    string title;

    public TitleSystem(string title) {
        this.title = title;
    }

    public void SetTitle(string title) {
        this.title = title;
    }

    public string GetTitle() {
        return title;
    }

    public void ShowIntro() {
        OnIntro?.Invoke(this, EventArgs.Empty);
    }
}
