using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogSystem : MonoBehaviour
{
    public event EventHandler OnIntro;
    public DialogSystem(){
        
    }

    public void ShowIntro(){
        OnIntro?.Invoke(this, EventArgs.Empty);
    }
}
