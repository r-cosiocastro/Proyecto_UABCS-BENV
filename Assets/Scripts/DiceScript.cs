using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceScript : MonoBehaviour {
    private TextMeshPro[] textMeshProUGUIs;
    LTDescr tween;
    public float x, y, z;

    private void Awake() {
        textMeshProUGUIs = GetComponentsInChildren<TextMeshPro>();
    }

    private void Start() {
        StartCoroutine(RandomNumber());
        // StartCoroutine(RotateRandom());
        // LeanTween.rotateAroundLocal(gameObject, Vector3.up, 90f, 1f).setLoopClamp();
        // 200, 270, 180
        tween = LeanTween.rotateLocal(gameObject, new Vector3(x, y, z), 0.8f).setEaseLinear().setLoopClamp();
    }



    // Update is called once per frame
    void Update() {
        //LeanTween.rotateLocal(gameObject, new Vector3(-170, -100, -190), 0.5f).setEaseLinear();
    }

    private IEnumerator RandomNumber() {
        while (true) {
            DisplayNumber(Random.Range(1, 11));
            yield return new WaitForSeconds(0.8f);
        }
    }

    private void DisplayNumber(int number) {
        foreach (TextMeshPro textComponent in textMeshProUGUIs) {
            textComponent.text = number.ToString();
        }
    }
}
