using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadMainMenu : MonoBehaviour {
    private SceneLoader sceneLoader;
    [SerializeField] AudioSource BGM;
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioMixerSnapshot mixer;

    private void Awake() {
        sceneLoader = SceneLoader._instance;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            sceneLoader.LoadScene("MainMenu", 1f);
            mixer.TransitionTo(2f);
            SFX.Play();
        }
    }
}
