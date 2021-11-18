using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    private SceneLoader sceneLoader;

    private void Awake() {
        sceneLoader = SceneLoader._instance;
    }

    public void LoadDemoScene() {
        sceneLoader.LoadScene("GameIndividual", 1f);
    }
}
