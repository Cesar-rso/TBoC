using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager : MonoBehaviour {

	public string language;
	public float volume;

	// Use this for initialization
	void Awake () {

		Load ();
		if(language.Equals("") || language == null){
			language = "english";
		}
		if (volume == null || volume < 0f) {
			volume = 1f;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Load (){
		if(File.Exists(Application.persistentDataPath + "/TBoC.dat")){
			BinaryFormatter format = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/TBoC.dat",FileMode.Open);

			GameData data = (GameData)format.Deserialize(file);
			file.Close();
			language = data.language;
			volume = data.volume;
		}
	}

	public void Save(){
		BinaryFormatter format = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/Fs.dat");

		GameData data = new GameData();
		data.language = language;
		data.volume = volume;

		format.Serialize(file,data);
		file.Close();
	}
}

[Serializable]
public class GameData{
	public string language;
	public float volume;
}
