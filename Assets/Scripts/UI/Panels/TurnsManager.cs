using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    private Animator animator;
    private Animator stateAnimOut;
    private GameUIManager gameUIManager;
    int currentTurn = 1;
    int finalTurn = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameUIManager = GameUIManager.instance;
        animator = GetComponent<Animator>();
        animator.Play("Intro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI(){
        turnText.text = string.Format("{0}/{1}",currentTurn,finalTurn);
    }

    void NextTurn(){
        currentTurn++;
        UpdateUI();
        animator.Play("Intro");
    }

    void AnimationCompleted(){
        gameUIManager.NextTurnAnimComplete();
    }
}
