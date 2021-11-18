using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {
    public static SceneLoader _instance;
    public Transform background;
    public AudioClip swipeOutClip;
    public AudioClip swipeInClip;
    public AudioSource audioSource;

    private void Awake() {
        if (!_instance) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string name, float delay = 0f) {
        StartCoroutine(LoadAsyncSceneString(name, delay));
    }

    public static void Hello() {

    }

    IEnumerator LoadAsyncSceneString(string name, float delay) {
        yield return new WaitForSeconds(delay);

        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        background.LeanMoveLocal(new Vector3(0, 0, 0), 1f).setEaseOutExpo().delay = 0.1f;
        audioSource.PlayOneShot(swipeOutClip);
        yield return new WaitForSeconds(1f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone) {
            yield return null;
        }

        background.LeanMoveLocal(new Vector3(-1843.884f, -1423.883f, 0), 1f).setEaseOutExpo().setOnComplete(RestartBackgroundPosition);
        audioSource.PlayOneShot(swipeInClip);
    }

    void RestartBackgroundPosition() {
        background.transform.localPosition = new Vector3(1843.884f, 1423.884f, 0);
    }
}
