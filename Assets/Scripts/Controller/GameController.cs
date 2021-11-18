using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using System;
using Cinemachine;
using TMPro;

public class GameController : MonoBehaviour {

    // TTS: Pedrito, busca la palabra <emph>mamá</emph>. <emph><spell>mama</emph></spell>. Mamá.
    // TMP: Pedrito,<p:short> busca la palabra <color=#1B1464><b>Mamá</b></color>.\n <p:long> <sp:6> <b><anim:wave>MAMÁ</anim>.<p:long> Mamá.</b>
    public static GameController instance;
    PlayerSystem playerSystem;
    TimeSystem timeSystem;
    TurnSystem turnSystem;
    TeacherControlSystem teacherControlSystem;
    DialogAvatarSystem dialogAvatarSystem;
    DialogSystem dialogSystem;
    StatsSystem statsSystem;
    TitleSystem titleSystem;
    StripSystem stripSystem;

    void Awake() {
        if (!GameController.instance) {
            GameController.instance = this;
        } else {
            Destroy(this.gameObject);
        }

        timeSystem = new TimeSystem();
        timeWindow.SetTimeSystem(timeSystem);

        turnSystem = new TurnSystem();
        turnWindow.SetTurnSystem(turnSystem);

        teacherControlSystem = new TeacherControlSystem();
        teacherControlWindow.SetTeacherControlSystem(teacherControlSystem);

        dialogAvatarSystem = new DialogAvatarSystem();
        dialogAvatarWindow.SetDialogAvatarSystem(dialogAvatarSystem);

        dialogSystem = new DialogSystem();
        dialogWindow.SetDialogSystem(dialogSystem);

        titleSystem = new TitleSystem("Juego individual - Nivel 1");
        titleWindow.SetTitleSystem(titleSystem);

        stripSystem = new StripSystem(turnSystem);
        stripWindow.SetStripSystem(stripSystem);
    }


    List<StudentEntity> studentsList = new List<StudentEntity>();
    List<ObjectEntity> objectsList = new List<ObjectEntity>();

    [SerializeField] bool CleanStudentTable = false;
    [SerializeField] bool CleanObjectsTable = false;
    [SerializeField] bool CleanResultsTable = false;

    [SerializeField] bool InsertNewStudents = false;

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

    [Header("Cámaras virtuales")]
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera deviceCamera;

    // Componentes de la UI
    [Header("Componentes de la UI")]
    [SerializeField] GameObject cardHolder;
    [SerializeField] CameraChanger cameraChanger;
    [SerializeField] ParticleSystem confettiParticles;
    [Header("Panels (windows)")]

    [SerializeField] private PlayerWindow playerWindow;
    [SerializeField] private TurnWindow turnWindow;
    [SerializeField] private TimeWindow timeWindow;
    [SerializeField] private TeacherControlWindow teacherControlWindow;
    [SerializeField] private DialogAvatarWindow dialogAvatarWindow;
    [SerializeField] private DialogWindow dialogWindow;
    [SerializeField] private StatsWindow statsWindow;
    [SerializeField] private TitleWindow titleWindow;
    [SerializeField] private StripWindow stripWindow;


    [SerializeField] GameObject cardHolder3D;
    [SerializeField] GameObject cardHolder3Dspriterenderer;
    [SerializeField] CanvasGroup canvasGroup;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip wrongSound;
    [SerializeField] AudioClip correctSound;
    [SerializeField] AudioClip cheersSound;
    [SerializeField] AudioClip kidsCheeringSound;
    [SerializeField] AudioClip applauseSound;

    // Database objects

    StudentDB studentDB;
    ObjectDB objectDB;
    ResultDB resultDB;


    // Índice del objeto actual en la lista
    private int currentObjectIndex = -1;
    // Índice del estudiante actual en la lista
    private int currentStudentIndex = -1;
    private string currentAnswerID;

    StudentEntity CurrentStudent() {
        return playerSystem.GetCurrentPlayer();
    }

    ObjectEntity CurrentObject() {
        return objectsList[currentObjectIndex];
    }

    void Start() {
        studentDB = new StudentDB();
        if (CleanStudentTable) {
            studentDB.deleteAllData();
            studentDB = new StudentDB();
        }

        objectDB = new ObjectDB();
        if (CleanObjectsTable) {
            objectDB.deleteAllData();
            objectDB = new ObjectDB();
        }

        resultDB = new ResultDB();
        if (CleanResultsTable) {
            resultDB.deleteAllData();
            resultDB = new ResultDB();
        }

        // DEMO

        if (InsertNewStudents) {

            // Inventar nombres aleatorios
            studentDB.addOrReplaceData(new StudentEntity(1, "Pablo", "Juárez", "Flores", "Pablito", 1, 0, 0));
            studentDB.addOrReplaceData(new StudentEntity(2, "Pedro Luis", "Castro", "Hernández", "Pedrito", 2, 0, 0));
            studentDB.addOrReplaceData(new StudentEntity(3, "Luis", "Esparza", "Rodríguez", "Luis", 3, 0, 0));
            studentDB.addOrReplaceData(new StudentEntity(4, "Raúl", "González", "Ortega", "Raúl", 4, 0, 0));
            //studentsList.Add(new StudentEntity(4, "Guadalupe", "Contreras", "Martínez","Lupita"));
        }

        //Fetch All Data
        System.Data.IDataReader readerStudent = studentDB.getAllData();

        while (readerStudent.Read()) {
            StudentEntity entity = new StudentEntity(readerStudent.GetInt32(0),
                                    readerStudent.GetString(1),
                                    readerStudent.GetString(2),
                                    readerStudent.GetString(3),
                                    readerStudent.GetString(4),
                                    readerStudent.GetInt32(5),
                                    readerStudent.GetString(6),
                                    readerStudent.GetInt32(7),
                                    readerStudent.GetInt32(8),
                                    readerStudent.GetInt32(9));

            Debug.Log(entity.ToString());

            studentsList.Add(entity);
        }

        readerStudent.Close();

        // Calcular los turnos (multiplicar número de rondas por número de alumnos)
        turnSystem.SetTotalTurns(studentsList.Count * roundNumbers);
        turnSystem.SetTotalRounds(roundNumbers);

        // Inventar tarjetas aleatorias
        objectDB.addOrReplaceData(new ObjectEntity("1", "Familia", "Papá", "Papá", 1));
        objectDB.addOrReplaceData(new ObjectEntity("2", "Familia", "Mamá", "Mamá", 1));
        objectDB.addOrReplaceData(new ObjectEntity("3", "Familia", "Hermano", "Hermano", 1));
        objectDB.addOrReplaceData(new ObjectEntity("4", "Familia", "Hermana", "Hermana", 1));

        //Fetch All Data
        System.Data.IDataReader readerObject = objectDB.getAllData();

        while (readerObject.Read()) {
            ObjectEntity entity = new ObjectEntity(readerObject.GetString(0),
                                    readerObject.GetString(1),
                                    readerObject.GetString(2),
                                    readerObject.GetString(3),
                                    readerObject.GetInt32(4));

            Debug.Log(entity.ToString());

            objectsList.Add(entity);
        }

        readerObject.Close();

        // Aletorizar orden de estudiantes u ordenarlos por número de lista
        if (orderStudentsRandomly) {
            studentsList = Shuffle(studentsList);
        } else {
            studentsList.Sort((x, y) => x._listNumber.CompareTo(y._listNumber));
        }

        playerSystem = new PlayerSystem(studentsList);
        playerWindow.SetPlayerSystem(playerSystem);

        statsSystem = new StatsSystem(playerSystem);
        statsWindow.SetStatsSystem(statsSystem);

        // Aletorizar orden objetos que se van a pedir
        objectsList = Shuffle(objectsList);

        // Dar la bienvenida
        // REMOVE avatarPanel.SetActive(true);
        dialogAvatarSystem.ShowIntro();
        // REMOVE dialogPanel.SetActive(true);
        dialogSystem.ShowIntro();
        FunctionTimer.Create(WelcomeDialog, 1f);

        // TODO: Tutorial

        /////////////////


    }

    void StartGame() {
        cameraChanger.ChangeToMainCamera();
        timeSystem.StartTimer();

        NextTurn();

        // Mostrar la primera imagen del primer objeto
        cardHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentObject()._name);
    }

    // Mostrar diálogo de bienvenida
    void WelcomeDialog() {
        dialogSystem.PlayDialogueText("Bienvenidos al juego del tablero.",
        "Bienvenidos al juego del tablero <sprite=5>.", 2f);
        dialogSystem.PlayDialogueText("Prepárate para colocar las tarjetas cuando escuches tu nombre.",
        "Prepárate para colocar las tarjetas cuando escuches tu nombre.", FirstGame, 2f);
    }

    void FirstGame() {
        titleSystem.ShowIntro();
        timeSystem.ShowIntro();
        turnSystem.ShowIntro();

        /* TODO: USE STRIP SYSTEM */

        FunctionTimer.Create(StartGame, 2f);
    }

    string PickRandomAskForObjectDialog() {
        string[] dialogArray = {
            "{0}, busca la palabra {2}. Buscamos a {1}.",
            "Es tu turno, {0}. Vamos a buscar la palabra {2}. Necesitamos a {1}.",
            "Vamos, {0}, hay que ganar. Coloca la palabra {2}. Traéme a {1}.",
            "Ahora estamos buscando la palabra {2}. ¿Podrás ayudarme a encontrarla, {0}?. Recuerda, {1}."
        };
        int rng = UnityEngine.Random.Range(0, dialogArray.Length);
        return dialogArray[rng];
    }

    void PutCardInReader(string id) {
        // Ocultar la interfaz moméntaneamente
        LeanTween.alphaCanvas(canvasGroup, 0f, 1f);

        cameraChanger.ChangeToDeviceCamera();

        currentAnswerID = id;

        StartCoroutine(CheckAnswer());
    }

    IEnumerator CheckAnswer() {
        //TODO: Cambiar ID por UUID (RFID)

        yield return new WaitForSeconds(1f);

        cardHolder3D.GetComponent<Animator>().SetTrigger("PutCard");
        //PlayAnimation(cardHolder3D, "PutCard");

        yield return new WaitForSeconds(3f);

        bool correctAnswer = currentAnswerID.Equals(CurrentObject()._id);
        /*
        foreach (ObjectEntity _object in objectsList){
            if(_object._id == id){
                correctAnswer = true;
                break;
            }
        }
        */

        LeanTween.alphaCanvas(canvasGroup, 1f, 1f);
        yield return new WaitForSeconds(2f);

        cameraChanger.ChangeToMainCamera();

        yield return new WaitForSeconds(2f);

        if (correctAnswer) {
            //CorrectAnswer();
            // REMOVE currentPlayerPanel.GetComponent<Animator>().SetTrigger("StarEarned");
            playerSystem.AddStar(1);
            StartConfettiParty();
            statsSystem.AddCorrectAnswer();
            sound.PlayOneShot(correctSound, 0.9f);
            sound.PlayOneShot(cheersSound, 0.4f);
            sound.PlayOneShot(kidsCheeringSound, 0.5f);
            sound.PlayOneShot(applauseSound, 0.7f);
            dialogSystem.PlayDialogueText("Felicidades, " + CurrentStudent()._nickname + ". Has colocado la palabra correcta. Te ganaste una estrella.",
            "Felicidades, " + StringUtils.DialogFormatStudentName(CurrentStudent()._nickname) + ". Has colocado la palabra correcta. Te ganaste una estrella. <sprite=5>");
            yield return new WaitForSeconds(9f);
            NextTurn();
        } else {
            //IncorrectAnswer();
            dialogSystem.PlayDialogueText(StringUtils.DialogFormatStudentName(CurrentStudent()._nickname) + ", esa no es la palabra correcta. Inténtalo otra vez.",
            CurrentStudent()._nickname + ", esa no es la palabra correcta <sprite=15>. Inténtalo otra vez.");
            StartCoroutine(StartPitch(backgroundMusic, 3f));
        }

        StopConfettiParty();
    }

    void StartConfettiParty() {
        confettiParticles.Play();
    }

    void StopConfettiParty() {
        confettiParticles.Stop();
    }

    void NextTurn() {
        // Cambiar objeto actual
        if (repeatUntilEveryCardIsShown) {
            currentObjectIndex++;
            if (currentObjectIndex >= objectsList.Count)
                currentObjectIndex = 0;
        } else {
            int oldObjectIndex = currentObjectIndex;
            do {
                currentObjectIndex = UnityEngine.Random.Range(0, objectsList.Count);
            } while (oldObjectIndex == currentObjectIndex);
        }

        // Cambiar imagen del objeto
        cardHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentObject()._name);

        // Cambiar estudiante actual
        // currentStudentIndex = currentStudentIndex + 1 >= studentsList.Count ? 0 : currentStudentIndex + 1;
        // int nextIndex = currentStudentIndex + 1 >= studentsList.Count ? 0 : currentStudentIndex + 1;

        playerSystem.NextPlayer();
        stripSystem.ChangeTurn(playerSystem.GetCurrentPlayer(), playerSystem.GetNextPlayer());
        // stripSystem.ChangeTurn(ref studentsList, currentStudentIndex, nextIndex);
        FunctionTimer.Create(turnSystem.NextTurn, 10f);
        //playerSystem.ChangePlayer(CurrentStudent());
        statsSystem.ChangeStats(playerSystem.GetCurrentPlayer(), CurrentObject());

        // Pedir el nuevo objeto
        FunctionTimer.Create(AskForObject, 10f);
    }

    // Pedirle al usuario actual que coloque la tarjeta en el tablero
    void AskForObject() {
        statsSystem.ShowIntro();
        // statsPanel.SetActive(true);
        //currentPlayerPanel.SetActive(true);
        //PlayAnimation(currentPlayerPanel, "Intro");
        playerSystem.ShowIntro();
        string randomDialog = PickRandomAskForObjectDialog();

        dialogSystem.PlayDialogueText(StringUtils.FormatTextTTS(randomDialog, CurrentObject()._name, CurrentStudent()._nickname),
            StringUtils.FormatTextDialog(randomDialog, CurrentObject()._name, CurrentStudent()._nickname));


    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PutCardInReader("1");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Papá");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            PutCardInReader("2");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mamá");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            PutCardInReader("3");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hermano");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            PutCardInReader("4");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hermana");
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            confettiParticles.Play();
        }

        if (Input.GetKeyDown(KeyCode.O)) {
            confettiParticles.Stop();
        }

    }

    IEnumerator StartPitch(AudioSource audioSource, float time) {
        float start = audioSource.pitch;
        float currentTime = 0;
        bool soundPlayed = false;

        while (currentTime < time) {
            currentTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(start, 0.05f, currentTime / time);
            if (currentTime > (time / 2) && !soundPlayed) {
                sound.PlayOneShot(wrongSound);
                soundPlayed = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        currentTime = 0;

        while (currentTime < time) {
            currentTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(0.05f, start, currentTime / (time / 3));
            yield return null;
        }
        audioSource.pitch = start;
        yield break;
    }

    public static List<T> Shuffle<T>(List<T> _list) {
        for (int i = 0; i < _list.Count; i++) {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
}
