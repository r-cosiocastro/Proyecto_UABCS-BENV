using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;

public class GameController : MonoBehaviour
{   
    [SerializeField] Image cardHolder;
    List<StudentEntity> studentsList = new List<StudentEntity>();
    List<ObjectEntity> objectsList = new List<ObjectEntity>();

    // Repetir las cartas hasta que se hayan mostrado todas
    // Esto asegura que el siguiente jugador no tendrá la misma tarjeta que el anterior
    [Header("Repetir las cartas hasta que se hayan mostrado todas")]
    [SerializeField] bool repeatUntilEveryCardIsShown = false;

    // Elegir el orden de los estudiantes al azar o por número de lista
    [Header("Elegir el orden de los estudiantes al azar")]
    [SerializeField] bool orderStudentsRandomly = true;
    // Número de rondas que se van a jugar
    [Header("Número de rondas que se van a jugar")]
    [SerializeField] int roundNumbers = 3;

    // Componentes de la UI

    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] TurnsManager turnsManager;

    // Índice del objeto actual en la lista
    private int currentObjectIndex = 0;
    // Índice del estudiante actual en la lista
    private int currentStudentIndex = 0;
    private int currentTurn = 1;
    private int totalTurns = 1;

    StudentEntity CurrentStudent(){
        return studentsList[currentStudentIndex];
    }

    ObjectEntity CurrentObject(){
        return objectsList[currentObjectIndex];
    }
    int CurrentTurn(){
        return currentTurn;
    }

    void Start()
    {
        // DEMO

        // Inventar nombres aleatorios
        studentsList.Add(new StudentEntity(1, "Pablo", "Juárez", "Flores", "Pablito"));
        studentsList.Add(new StudentEntity(2, "Pedro Luis", "Castro", "Hernández","Pedrito"));
        studentsList.Add(new StudentEntity(3, "Luis", "Esparza", "Rodríguez", "Luis"));
        studentsList.Add(new StudentEntity(4, "Guadalupe", "Contreras", "Martínez","Lupita"));

        // Mostrar los nombres de los estudiantes en la consola
        foreach (StudentEntity student in studentsList){
            Debug.LogFormat("Alumno {0}: {1} {2} {3}", student._id, student._name, student._lastName1, student._lastName2);
        }

        // Calcular los turnos (multiplicar número de rondas por número de alumnos)
        totalTurns = studentsList.Count * roundNumbers;

        // Inventar tarjetas aleatorias
        objectsList.Add(new ObjectEntity("1","Familia","Papá", "Papá"));
        objectsList.Add(new ObjectEntity("2","Familia","Mamá", "Mamá"));
        objectsList.Add(new ObjectEntity("3","Familia","Hermano", "Hermano"));
        objectsList.Add(new ObjectEntity("4","Familia","Hermana", "Hermana"));

        //Mostrar los nombres de las tarjetas en consola
        foreach (ObjectEntity _object in objectsList){
            Debug.LogFormat("Tarjeta {0}: {1}",_object._id, _object._name);
        }

        // Aletorizar orden de estudiantes u ordenarlos por número de lista
        if(orderStudentsRandomly){
            studentsList = Shuffle(studentsList);
        }else{
            studentsList.Sort((x, y) => x._listNumber.CompareTo(y._listNumber));
        }

        // Aletorizar orden objetos que se van a pedir
        objectsList = Shuffle(objectsList);

        // Dar la bienvenida
        WelcomeDialog();

        // TODO: Tutorial
        
        /////////////////

        // Comenzar juego
        StartGame();
    }

    void StartGame(){
        // Mostrar el número del turno actual
        ShowCurrentTurn();

        // Pedir al usuario que coloque la tarjeta
        AskForObject();

        // Mostrar la primera imagen del primer objeto
        cardHolder.sprite = Resources.Load<Sprite>(CurrentObject()._name);
    }

    // Mostrar diálogo de bienvenida
    void WelcomeDialog(){
        dialogueManager.PlayDialogueText("Bienvenido al juego. Prepárate para colocar las tarjetas cuando escuches tu nombre.");
        Debug.Log("Bienvenidos al juego _____. Prepárense para colocar sus tarjetas cuando escuchen su nombre");
    }

    void CheckAnswer(string id){
        //TODO: Cambiar ID por UUID (RFID)
            bool correctAnswer = id.Equals(CurrentObject()._id);
            /*
            foreach (ObjectEntity _object in objectsList){
                if(_object._id == id){
                    correctAnswer = true;
                    break;
                }
            }
            */

            if (correctAnswer){
                CorrectAnswer();
                NextTurn();
            }else{
                IncorrectAnswer();
            }
    }

    // Respuesta correcta
    void CorrectAnswer(){
        Debug.Log("Felicidades, tienes la respuesta correcta.");
    }

    // Respuesta incorrecta
    void IncorrectAnswer(){
        Debug.Log("Todo menso, " + CurrentStudent()._nickname + " esa respuesta no es correcta, inténtalo de nuevo.");
    }

    void NextTurn(){
        // Cambiar objeto actual
        if(repeatUntilEveryCardIsShown){
            currentObjectIndex++;
            if(currentObjectIndex >= objectsList.Count)
                currentObjectIndex = 0;
        }else{
            int oldObjectIndex = currentObjectIndex;
            do{
                currentObjectIndex = Random.Range(0, objectsList.Count);
            }while(oldObjectIndex == currentObjectIndex);
        }

        // Cambiar imagen del objeto
        cardHolder.sprite = Resources.Load<Sprite>(CurrentObject()._name);

        // Cambiar estudiante actual
        currentStudentIndex++;
        if(currentStudentIndex >= studentsList.Count)
                currentStudentIndex = 0;                


        Debug.Log("currentObjectIndex: " + currentObjectIndex);
        Debug.Log("currentStudentIndex: " + currentStudentIndex);

        // Sumar +1 al turno actual
        currentTurn++;

        // Mostrar nuevo turno
        ShowCurrentTurn();
        turnsManager.ChangeTurn(currentTurn, totalTurns);

        // Pedir el nuevo objeto
        AskForObject();
    }

    // Mostrar el nuevo turno
    void ShowCurrentTurn(){
        Debug.LogFormat("Turno: {0}/{1}", CurrentTurn(), totalTurns);
    }

    // Pedirle al usuario actual que coloque la tarjeta en el tablero
    void AskForObject(){
        Debug.Log(CurrentStudent()._nickname+ ", coloca la tarjeta " + CurrentObject()._name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            CheckAnswer("1");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            CheckAnswer("2");
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            CheckAnswer("3");
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            CheckAnswer("4");
        }
    }

    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
}
