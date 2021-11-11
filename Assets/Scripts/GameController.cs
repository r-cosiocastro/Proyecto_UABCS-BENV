using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using System;
using Cinemachine;
using TMPro;

public class GameController : MonoBehaviour
{   

    // TTS: Pedrito, busca la palabra <emph>mamá</emph>. <emph><spell>mama</emph></spell>. Mamá.
    // TMP: Pedrito,<p:short> busca la palabra <color=#1B1464><b>Mamá</b></color>.\n <p:long> <sp:6> <b><anim:wave>MAMÁ</anim>.<p:long> Mamá.</b>
    public static GameController instance;
    // Start is called before the first frame update

    void Awake(){
        if(!GameController.instance){
            GameController.instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    
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

    [Header("Cámaras virtuales")]
    [SerializeField] CinemachineVirtualCamera mainCamera;
    [SerializeField] CinemachineVirtualCamera deviceCamera;

    // Componentes de la UI
    [Header("Componentes de la UI")]
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] TurnsManager turnsManager;
    [SerializeField] TimerScript timerComponent;
    [SerializeField] GameObject cardHolder;
    [SerializeField] CameraChanger cameraChanger;
    [SerializeField] ParticleSystem confettiParticles;
    [Header("Paneles (animaciones)")]
    [SerializeField] GameObject levelTitlePanel;
    [SerializeField] GameObject computerAvatarPanel;
    [SerializeField] GameObject teacherControlsPanel;
    [SerializeField] GameObject timerPanel;
    [SerializeField] GameObject avatarPanel;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] GameObject turnInformationPanel;
    [SerializeField] GameObject currentPlayerPanel;
    [SerializeField] GameObject nextPlayerPanel;
    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject cardHolder3D;
    [SerializeField] GameObject cardHolder3Dspriterenderer;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] CanvasGroup canvasGroup3D;

    [SerializeField] TextMeshProUGUI currentNicknameText;
    [SerializeField] TextMeshProUGUI nextNicknameText;
    [SerializeField] TextMeshProUGUI currentStatText;
    [SerializeField] TextMeshProUGUI dialogText;

    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource sound;
    [SerializeField] AudioClip wrongSound;

    



    // Índice del objeto actual en la lista
    private int currentObjectIndex = 0;
    // Índice del estudiante actual en la lista
    private int currentStudentIndex = 0;
    private int currentTurn = 1;
    private int totalTurns = 1;
    private string currentAnswerID;

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
        studentsList.Add(new StudentEntity(4, "Raúl", "González", "Ortega", "Raúl"));
        //studentsList.Add(new StudentEntity(4, "Guadalupe", "Contreras", "Martínez","Lupita"));

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
        avatarPanel.SetActive(true);
        dialogPanel.SetActive(true);
        Invoke("WelcomeDialog",1f);
        //WelcomeDialog();

        // TODO: Tutorial
        
        /////////////////

        // Comenzar juego 

        // Ahora se hará a través de onFinished del diálogo
        // StartGame();
    }

    private Coroutine typeRoutine = null;
    void DelayVoid(Action onFinish, float delayEnd) {
        this.EnsureCoroutineStopped(ref typeRoutine);
        typeRoutine = StartCoroutine(OnFinishAnimationCallback(onFinish, delayEnd));
    }

	IEnumerator OnFinishAnimationCallback(Action onFinish, float delayEnd){
		yield return new WaitForSeconds(delayEnd);
		onFinish?.Invoke();
	}

    void PlayAnimation(GameObject gameObject, string animation){
        gameObject.GetComponent<Animator>().Play(animation);
    }

    void StartGame(){
        cameraChanger.ChangeToMainCamera();
        // Comenzar el crónometro
        timerComponent.StartTimer();
        PlayAnimation(timerPanel, "StartTimer");

        // Mostrar el número del turno actual
        ShowCurrentTurn();

        // Pedir al usuario que coloque la tarjeta
        AskForObject();

        // Mostrar la primera imagen del primer objeto
        cardHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentObject()._name);
    }

    // Mostrar diálogo de bienvenida
    void WelcomeDialog(){
        dialogueManager.PlayDialogueText("Bienvenidos al juego del tablero. Prepárate para colocar las tarjetas cuando escuches tu nombre.",
        "Bienvenidos al juego del tablero <sprite=5>. Prepárate para colocar las tarjetas cuando escuches tu nombre.", FirstGame, 2f);
        Debug.Log("Bienvenidos al juego _____. Prepárense para colocar sus tarjetas cuando escuchen su nombre");
    }

    void FirstGame(){
        levelTitlePanel.SetActive(true);
        timerPanel.SetActive(true);
        turnInformationPanel.SetActive(true);
        currentNicknameText.text = CurrentStudent()._nickname;
        currentStatText.text = CurrentStudent()._nickname;
        if(currentStudentIndex + 1 > studentsList.Count)
        nextNicknameText.text = studentsList[0]._nickname;
        else
        nextNicknameText.text = studentsList[currentStudentIndex + 1]._nickname;

        DelayVoid(StartGame, 2f);
    }

    string PickRandomAskForObjectDialog(){
        string[] dialogArray = {
            "{0}, busca la palabra {2}. {3}. {1}.",
            "Es tu turno, {0}. Vamos a buscar la palabra {2}. {3}. {1}.",
            "Vamos, {0}, hay que ganar. Coloca la palabra {2}. {3}. {1}.",
            "Ahora estamos buscando la palabra {2}. ¿Podrás ayudarme a encontrarla, {0}?. Recuerda, {3}. {1}."
        };
        int rng = UnityEngine.Random.Range(0,dialogArray.Length);
		return dialogArray[rng];
	}

    void PutCardInReader(string id){
        // Ocultar la interfaz moméntaneamente
            LeanTween.alphaCanvas(canvasGroup, 0f, 1f);

            cameraChanger.ChangeToDeviceCamera();

            currentAnswerID = id;

            StartCoroutine(CheckAnswer());
    }

    IEnumerator CheckAnswer(){
        //TODO: Cambiar ID por UUID (RFID)

        yield return new WaitForSeconds(1f);

        cardHolder3D.GetComponent<Animator>().SetTrigger("PutCard");
        //PlayAnimation(cardHolder3D, "PutCard");

        dialogText.text = "";

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

            LeanTween.alphaCanvas(canvasGroup3D, 1f, 0.5f);
            LeanTween.alphaCanvas(canvasGroup, 1f, 1f);
            yield return new WaitForSeconds(2f);

            cameraChanger.ChangeToMainCamera();

            yield return new WaitForSeconds(2f);

            if (correctAnswer){
                //CorrectAnswer();
                currentPlayerPanel.GetComponent<Animator>().SetTrigger("StarEarned");
                StartConfettiParty();
                dialogueManager.PlayDialogueText("Felicidades, " + CurrentStudent()._nickname+ ". Has colocado la palabra correcta. Te ganaste una estrella.", 
                "Felicidades, " + StringUtils.DialogFormatStudentName(CurrentStudent()._nickname)+ ". Has colocado la palabra correcta. Te ganaste una estrella. <sprite=5>", NextTurn);
                yield return new WaitForSeconds(5f);
            }else{
                //IncorrectAnswer();
                dialogueManager.PlayDialogueText(StringUtils.DialogFormatStudentName(CurrentStudent()._nickname)+ ", esa no es la palabra correcta. Inténtalo otra vez.",
                CurrentStudent()._nickname+ ", esa no es la palabra correcta <sprite=15>. Inténtalo otra vez.");
                StartCoroutine(StartPitch(backgroundMusic, 3f));
            }
            
            

            yield return new WaitForSeconds(8f);
            StopConfettiParty();
            LeanTween.alphaCanvas(canvasGroup3D, 0f, 1f);
    }

    // Respuesta correcta
    void CorrectAnswer(){
        StartConfettiParty();
        Debug.Log("Felicidades, tienes la respuesta correcta.");
    }

    void StartConfettiParty(){
        confettiParticles.Play();
    }

    void StopConfettiParty(){
        confettiParticles.Stop();
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
                currentObjectIndex = UnityEngine.Random.Range(0, objectsList.Count);
            }while(oldObjectIndex == currentObjectIndex);
        }

        // Cambiar imagen del objeto
        cardHolder.GetComponent<Image>().sprite = Resources.Load<Sprite>(CurrentObject()._name);

        // Cambiar estudiante actual
        currentStudentIndex++;
        if(currentStudentIndex >= studentsList.Count)
                currentStudentIndex = 0;                
        
        currentNicknameText.text = CurrentStudent()._nickname;
        currentStatText.text = CurrentStudent()._nickname;
        if(currentStudentIndex + 1 > studentsList.Count)
        nextNicknameText.text = studentsList[0]._nickname;
        else
        nextNicknameText.text = studentsList[currentStudentIndex + 1]._nickname;
        

        Debug.Log("currentObjectIndex: " + currentObjectIndex);
        Debug.Log("currentStudentIndex: " + currentStudentIndex);

        PlayAnimation(currentPlayerPanel, "Outro");
        PlayAnimation(nextPlayerPanel, "Outro");
        PlayAnimation(statsPanel, "Outro");

        // Sumar +1 al turno actual
        currentTurn++;

        // Mostrar nuevo turno
        ShowCurrentTurn();



        // Pedir el nuevo objeto
        //AskForObject();
        Invoke("AskForObject",3f);
    }

    // Mostrar el nuevo turno
    void ShowCurrentTurn(){
        turnsManager.ChangeTurn(currentTurn, totalTurns);
        PlayAnimation(turnInformationPanel, "Change");
        Debug.LogFormat("Turno: {0}/{1}", CurrentTurn(), totalTurns);
    }

    // Pedirle al usuario actual que coloque la tarjeta en el tablero
    void AskForObject(){
        statsPanel.SetActive(true);
        currentPlayerPanel.SetActive(true);
        nextPlayerPanel.SetActive(true);
        PlayAnimation(currentPlayerPanel, "Intro");
        PlayAnimation(nextPlayerPanel, "Intro");
        PlayAnimation(statsPanel, "Intro");
        string randomDialog = PickRandomAskForObjectDialog();

        dialogueManager.PlayDialogueText(StringUtils.FormatTextTTS(randomDialog, CurrentObject()._name, CurrentStudent()._nickname),
			StringUtils.FormatTextDialog(randomDialog, CurrentObject()._name, CurrentStudent()._nickname));


        Debug.Log(CurrentStudent()._nickname+ ", coloca la tarjeta " + CurrentObject()._name);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            PutCardInReader("1");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Papá");
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            PutCardInReader("2");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Mamá");
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)){
            PutCardInReader("3");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hermano");
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)){
            PutCardInReader("4");
            cardHolder3Dspriterenderer.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Hermana");
        }
        if(Input.GetKeyDown(KeyCode.P)){
            confettiParticles.Play();
        }

        if(Input.GetKeyDown(KeyCode.O)){
            confettiParticles.Stop();
        }
        
    }

    IEnumerator StartPitch(AudioSource audioSource, float time)
    {
        float start = audioSource.pitch;
        float currentTime = 0;
        bool soundPlayed = false;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(start, 0.05f, currentTime / time);
            if(currentTime > (time / 2) && !soundPlayed)
            {
                sound.PlayOneShot(wrongSound);
                soundPlayed = true;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        currentTime = 0;

        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            audioSource.pitch = Mathf.Lerp(0.05f, start, currentTime / (time / 3));
            yield return null;
        }
        audioSource.pitch = start;
        yield break;
    }

    public static List<T> Shuffle<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            T temp = _list[i];
            int randomIndex = UnityEngine.Random.Range(i, _list.Count);
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }

        return _list;
    }
}
