using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using System;
using System.Diagnostics;
using UnityEngine.Diagnostics;

public class TitleScreenController : MonoBehaviour {
    private SceneLoader sceneLoader;
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioMixerSnapshot mixer;
    [SerializeField] Animator instructionsPanelAnimator;
    [SerializeField] Animator logoTextAnimator;
    [SerializeField] TextMeshProUGUI debugLabel;

    private void Start() {
        sceneLoader = SceneLoader._instance;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            LoadMainMenu();
        }
    }

    public void LoadMainMenu() {
        debugLabel.text = "Loading scene...";
        try {
            sceneLoader.LoadScene("MainMenu", 1f);
        } catch (Exception e) {
            debugLabel.text = e.Message;
        }
        mixer.TransitionTo(2f);
        SFX.Play();
        instructionsPanelAnimator.SetTrigger("KeyPressed");
        logoTextAnimator.SetTrigger("KeyPressed");
    }
}
