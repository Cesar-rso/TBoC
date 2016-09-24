using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LanguageManager : MonoBehaviour {

	public GameObject TextObject;
	public GameObject datamanager;
	public string currentLanguage="english", english, portugues;

	// Use this for initialization
	void Start () {
		currentLanguage = datamanager.GetComponent<DataManager> ().language;
	}
	
	// Update is called once per frame
	void Update () {
		if(datamanager.GetComponent<DataManager> ().language != currentLanguage){
			Translate ();
		}
	}

	public void Translate(){
		currentLanguage = datamanager.GetComponent<DataManager> ().language;
		if(currentLanguage.Equals("english") || currentLanguage.Equals("English")){
			TextObject.GetComponent<Text> ().text = english;
		}
		if(currentLanguage.Equals("portugues") || currentLanguage.Equals("Portugues") || currentLanguage.Equals("português") || currentLanguage.Equals("Português")){
			TextObject.GetComponent<Text> ().text = portugues;
		}

		//datamanager.GetComponent<DataManager> ().language = currentLanguage;
	}

	public void ChangeLanguage(string newLanguage){
		currentLanguage = newLanguage;
		//Translate ();
	}
}
