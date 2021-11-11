using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;
    private Animator animator;
    private Animator stateAnimOut;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        animator = GetComponent<Animator>();
    }

    public void ChangeTurn(int currentTurn, int finalTurn){
        turnText.text = string.Format("{0}/{1}",currentTurn,finalTurn);
        //animator.Play("Intro");
    }

    void AnimationCompleted(){
        //gameController.NextTurnAnimComplete();
    }
}
