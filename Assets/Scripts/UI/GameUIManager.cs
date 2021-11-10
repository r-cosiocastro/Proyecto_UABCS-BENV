using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    public static GameUIManager instance;
    // Start is called before the first frame update

    void Awake(){
        if(!GameUIManager.instance){
            GameUIManager.instance = this;
        }else{
            Destroy(this.gameObject);
        }
    }

    [SerializeField] GameObject turnInfoPanel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextTurnAnimComplete(){
        // Debug.Log("Next turn animation complete");
    }
}
