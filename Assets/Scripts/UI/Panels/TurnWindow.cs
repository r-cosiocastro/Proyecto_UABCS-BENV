using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnWindow : MonoBehaviour
{
    private TextMeshProUGUI turnText;
    private TurnSystem turnSystem;
    private CanvasGroup canvasGroup;
    private void Awake() {
        turnText = transform.Find("TurnInformationTextPanel").Find("CurrentTurnText").GetComponent<TextMeshProUGUI>();
    }

    public void SetTurnSystem(TurnSystem turnSystem){
        this.turnSystem = turnSystem;

        //Events
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e){
        GetComponent<Animator>().Play("Change");
        turnText.text = turnSystem.GetCurrentTurnString();
    }
}
