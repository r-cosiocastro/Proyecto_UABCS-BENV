using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowCameraRotation : MonoBehaviour {
    Skybox box;
    public float speed = 5f;

    void Start() {
        box = GetComponent<Skybox>();
    }

    void LateUpdate() {
        box.material.SetFloat("_Rotation", Time.time * speed);
    }
}