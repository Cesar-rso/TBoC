using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}
	
	public void OnClick () {
		if(transform.name.Contains("Start")){
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}

		if(transform.name.Contains("Credits")){
			SceneManager.LoadScene("CreditsScene");
		}

		if(transform.name.Contains("Exit")){
			Application.Quit();
		}
			
	}
		
}
