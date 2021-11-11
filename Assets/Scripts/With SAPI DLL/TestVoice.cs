// **************************************************************************
// Unity Text To Speech V2 ZJP. Test Voice
// **************************************************************************
using UnityEngine;
using System; 

public class TestVoice : MonoBehaviour
{
	public int voiceOk;
	public int numVoice;
	public string voiceName;
	private VoiceManager vm;
	[SerializeField] DialogueManager dialogueManager;
	
	public string textToSay;

	string CurrentObject = "Papá";
	string CurrentStudent = "Pablito";

	void Start ()
	{
		vm = VoiceManager.instance;
		numVoice  = vm.VoiceNumber;
		voiceName = vm.VoiceNames[0];
		Debug.Log("Voice number " + vm.VoiceNumber);
		//dialogueManager.PlayDialogueText
		//textesay = "<voice required='Name=Microsoft Sabina Desktop'> Hola, Rafael";

		//String textToSay = "mamú. mama. <emph><spell>mamá</emph></spell>. <emph><spell>áóú</emph></spell>";

		
		// textesay = "<voice required='" + voiceName + "'>" + sayThisText;
		// textesay = "<voice required='" + vm.VoiceNames[i] + "'>" + sayThisText;
        //textesay = "<voice required='Name=Microsoft Helena Desktop'> Hola Luis";
		//vm.Say(textesay); // start a speech
		//vm.SayEX("c:/text.txt",12);
		//vm.SayEX("Hello, this is the default voice. The one", 1+8);
		//vm.SayEX("<voice required='Name=" + voiceName + "'>" + "Hello, this is the voice 2", 1+8+32);
		//vm.SayEX("Funny, it's the voice 2 again", 1+8);
	}

	void FinishSpeaking(){
		Debug.Log("Ya terminó de hablar");
	}

	string PickRandomDialog(){
        string[] dialogArray = {
            "{0}, busca la palabra {2}. {3}. {1}.",
            "Ahora es tu turno, {0}. Vamos a buscar la palabra {2}. {3}. {1}.",
            "Vamos, {0}, hay que ganar. Coloca la palabra {2}. {3}. {1}.",
            "Ahora estamos buscando la palabra {2}. ¿Podrás ayudarme a encontrarla, {0}. Recuerda, {3}. {1}."
        };
        int rng = UnityEngine.Random.Range(0,dialogArray.Length);
		return dialogArray[rng];
	}

	string FormatTextDialog(string formatThisString){
		return string.Format(formatThisString,
				DialogFormatStudentName(), 
				CurrentObject, 
				DialogFormatObjectName(),
				DialogFormatObjectSpell());
	}
	string FormatTextTTS(string formatThisString){
		return string.Format(formatThisString,
				CurrentStudent, 
				CurrentObject, 
				TTSEmphObjectName(),
				TTSSpellObjectName());
	}

    string TTSSpellObjectName(){
        return "<emph><spell>"+CurrentObject+"</emph></spell>";
    }
    
    string TTSEmphObjectName(){
        return "<emph>"+CurrentObject+"</emph>";
    }

    string DialogFormatObjectSpell(){
        return "<sp:6><b><anim:wave>"+CurrentObject.ToUpper()+"</anim></b>";
    }

    string DialogFormatStudentName(){
        return "<color=#2980B9>"+CurrentStudent+"</color>";
    }

    string DialogFormatObjectName(){
        return "<color=#D35400><b>"+ CurrentObject+"</b></color>";
    }

	// Update is called once per frame
	void Update ()
	{
		/*
		if (vm.Status(0) == 2 ) // a speech is running
		{
			Debug.Log(" Total Stream  > " + vm.Status(2));
			Debug.Log(" Actual stream <<<<<<<<<<<<<<<<<<<<<<<<<<<<<> " + vm.Status(3));
			Debug.Log(" Position of the actual spoken word in the actual stream > " + vm.Status(1));
		}
		*/
		if(Input.GetKeyDown(KeyCode.S)){
			//vm.Speak ("Pedrito, busca la palabra <emph> mamá </emph>. <emph><spell>mama</emph></spell>. Mamá.", FinishSpeaking);

			string randomDialog = PickRandomDialog();


			dialogueManager.PlayDialogueText(FormatTextTTS(randomDialog),
			FormatTextDialog(randomDialog));
		}
	}
	
}
