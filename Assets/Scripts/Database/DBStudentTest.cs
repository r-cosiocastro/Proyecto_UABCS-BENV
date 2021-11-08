using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Database;
using TMPro;

public class DBStudentTest : MonoBehaviour
{
    [SerializeField] bool CleanTable;
    

    [SerializeField] GameObject UIPanel;

    [Header("UI Components to fill")] 

    [SerializeField] TextMeshProUGUI studentNameTitle;
    [SerializeField] TMP_InputField studentName;
    [SerializeField] TMP_InputField studentLastName1;
    [SerializeField] TMP_InputField studentLastName2;
    [SerializeField] TMP_InputField studentNickname;
    [SerializeField] TMP_InputField studentListNumber;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject scrollView;

    [Header("Notification Components")] 
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] Sprite okayIcon;
    [SerializeField] Sprite infoIcon;
    [SerializeField] Sprite errorIcon;
    [SerializeField] AudioClip errorSound;
    [SerializeField] AudioClip infoSound;
    [SerializeField] AudioClip okaySound;


    private StudentEntity selectedStudent;
    // Start is called before the first frame update
    void Start()
    {
        StudentDB studentDB = new StudentDB();

        if(CleanTable){
            studentDB.deleteAllData();
            studentDB = new StudentDB();
        }

        studentDB.addOrReplaceData(new StudentEntity("Yohana Karely", "Castro", "Flores", "Yojana", 3));
        studentDB.addOrReplaceData(new StudentEntity(0, "Rafael Alberto", "Cosio", "Castro", "Rafa", 6));
        studentDB.addOrReplaceData(new StudentEntity(0, "Francisco Javier", "Murillo", "Castro", "Javi", 5, "A", 2));

        studentDB.close();

        //Fetch All Data
        StudentDB studentDB2 = new StudentDB();
        System.Data.IDataReader reader = studentDB2.getAllData();

        int fieldCount = reader.FieldCount;
        List<StudentEntity> myList = new List<StudentEntity>();
        while (reader.Read())
        {
            StudentEntity entity = new StudentEntity(reader.GetInt32(0),
                                    reader.GetString(1),
                                    reader.GetString(2),
                                    reader.GetString(3),
                                    reader.GetString(4),
                                    reader.GetInt32(5),
                                    reader.GetString(6),
                                    reader.GetInt32(7));

            Debug.Log("id: " + entity._id 
            + ", name: " + entity._name 
            + ", lastName1: " + entity._lastName1 
            + ", lastName2: " + entity._lastName2 
            + ", nickname: " + entity._nickname 
            + ", listNumber: " + entity._listNumber 
            + ", classroom: " + entity._classroom 
            + ", grade: " + entity._grade);

        GameObject go = Instantiate(buttonPrefab) as GameObject; 
        //go.transform.parent = scrollView.transform;
        go.transform.SetParent(scrollView.transform, false);
        go.GetComponentInChildren<TextMeshProUGUI>().text = entity._name + " " + entity._lastName1 + " " + entity._lastName2;

        Button ButtonPre = go.GetComponent<Button>();
        ButtonPre.onClick.AddListener(delegate{FillStudentData(entity);});

            myList.Add(entity);
        }
        studentDB2.close();

        // Instanciar botones
        
    }
    
    void Notification(string title, string description, Sprite icon, AudioClip sound){
        
    }

    void FillStudentData(StudentEntity studentEntity){
        studentNameTitle.text = studentEntity._name + " " + studentEntity._lastName1 + " " + studentEntity._lastName2;
        studentName.text = studentEntity._name;
        studentLastName1.text = studentEntity._lastName1;
        studentLastName2.text = studentEntity._lastName2;
        studentListNumber.text = studentEntity._listNumber + "";
        studentNickname.text = studentEntity._nickname;
        selectedStudent = studentEntity;
        Debug.Log(selectedStudent.ToString());

        Debug.Log("id: " + studentEntity._id 
            + ", name: " + studentEntity._name 
            + ", lastName1: " + studentEntity._lastName1 
            + ", lastName2: " + studentEntity._lastName2 
            + ", nickname: " + studentEntity._nickname 
            + ", listNumber: " + studentEntity._listNumber 
            + ", classroom: " + studentEntity._classroom 
            + ", grade: " + studentEntity._grade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
