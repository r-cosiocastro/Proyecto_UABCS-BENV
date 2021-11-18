using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HueShifter : MonoBehaviour {
    public float Speed = 1;
    private TextMeshProUGUI rend;

    void Start() {
        rend = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        rend.material.color = new Color32(255, 255, 255, 255);
    }
}
