using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Database;
using TMPro;

public class TestStrip : MonoBehaviour {
    [SerializeField] StripWindow stripWindow;
    private StripSystem stripSystem;
    private TurnSystem turnSystem;
    [SerializeField] TextMeshProUGUI currentRound;
    List<StudentEntity> studentsList = new List<StudentEntity>();
    int studentIndex;

    private void Awake() {
        turnSystem = new TurnSystem(12);
        //stripSystem = new StripSystem(turnSystem);
        stripWindow.SetStripSystem(stripSystem);
    }

    // Start is called before the first frame update
    void Start() {

        studentsList.Add(new StudentEntity(1, "Pablo", "Juárez", "Flores", "Pablito", 1, 999, 99));
        studentsList.Add(new StudentEntity(2, "Pedro Luis", "Castro", "Hernández", "Pedrito", 2, 999, 99));
        studentsList.Add(new StudentEntity(3, "Luis", "Esparza", "Rodríguez", "Luis", 3, 999, 99));
        studentsList.Add(new StudentEntity(4, "Raúl", "González", "Ortega", "Raúl", 4, 999, 99));

        studentIndex = -1;
    }

    void ChangePlayer() {
        studentIndex = studentIndex + 1 >= studentsList.Count ? 0 : studentIndex + 1;
        int nextIndex = studentIndex + 1 >= studentsList.Count ? 0 : studentIndex + 1;
        turnSystem.NextTurn();

        Debug.Log("Current index: " + studentIndex);
        Debug.Log("Next index: " + nextIndex);

        currentRound.text = "Ronda: " + turnSystem.GetCurrentTurn();
        //stripSystem.ChangeTurn(ref studentsList, studentIndex, nextIndex);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            ChangePlayer();
        }
    }
}
