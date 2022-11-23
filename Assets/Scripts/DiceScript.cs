using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceScript : MonoBehaviour {
    private TextMeshPro[] textMeshProUGUIs;
    private SpriteRenderer[] spriteRenderers;
    int tween;
    public float duration = 0.8f;
    public float x, y, z;
    bool dicePunched = false;
    [SerializeField] AudioClip dicePunchedClip;
    [SerializeField] ParticleSystem particles;
    [SerializeField] GameObject textTop;
    [SerializeField] Sprite[] sprites;

    private void Awake() {
        textMeshProUGUIs = transform.Find("TextGroup").GetComponentsInChildren<TextMeshPro>();
    }

    private void Start() {
        StartCoroutine(RandomNumber());
        // StartCoroutine(RotateRandom());
        // LeanTween.rotateAroundLocal(gameObject, Vector3.up, 90f, 1f).setLoopClamp();
        // 180, 270, 180
        // original: tween = LeanTween.rotateLocal(gameObject, new Vector3(x, y, z), 0.833f).setRepeat(-1).setEaseSpring().setLoopClamp().id;

        tween = LeanTween.rotateLocal(gameObject, new Vector3(x, y, z), duration).setRepeat(-1).setLoopClamp().id;
        //LeanTween.scale(gameObject, new Vector3(0.5f, 0.5f, 0.5f),1f);
    }



    // Update is called once per frame
    void Update() {
        //LeanTween.rotateLocal(gameObject, new Vector3(-170, -100, -190), 0.5f).setEaseLinear();
        if (Input.GetKeyDown(KeyCode.Space)) {
            LeanTween.cancel(tween);
            DisplayNumber(Random.Range(1, 11));
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
            LeanTween.moveLocalY(gameObject, 1f, 1f).setEasePunch();
            LeanTween.moveLocalY(textTop, 1f, 0.5f).setEaseOutBack();
            dicePunched = true;
            particles.Play();


        }
    }

    private IEnumerator RandomNumber() {
        while (!dicePunched) {
            DisplayNumber(Random.Range(1, 11));
            yield return new WaitForSeconds(duration / 2f);
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(dicePunchedClip);
    }

    private IEnumerator RandomImage() {
        while (!dicePunched) {
            DisplayNumber(Random.Range(1, 11));
            yield return new WaitForSeconds(duration / 2f);
        }
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(dicePunchedClip);
    }

    private void DisplayImage(int index) {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers) {
            spriteRenderer.sprite = sprites[index];
        }
    }

    private void DisplayNumber(int number) {
        foreach (TextMeshPro textComponent in textMeshProUGUIs) {
            textComponent.text = number.ToString();
        }
    }
}
