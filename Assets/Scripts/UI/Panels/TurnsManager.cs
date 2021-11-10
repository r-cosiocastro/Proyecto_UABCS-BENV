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
    // Start is called before the first frame update
    void Start()
    {
        gameUIManager = GameUIManager.instance;
        animator = GetComponent<Animator>();
    }

    public void ChangeTurn(int currentTurn, int finalTurn){
        turnText.text = string.Format("{0}/{1}",currentTurn,finalTurn);
        animator.Play("Intro");
    }

    void AnimationCompleted(){
        gameUIManager.NextTurnAnimComplete();
    }
}
