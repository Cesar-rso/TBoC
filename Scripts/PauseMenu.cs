using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour {


	public Canvas PauseCanvas;

	// Use this for initialization
	void Start () {
		if(PauseCanvas != null){
			PauseCanvas.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Pause (){
		if (PauseCanvas != null) {
			if (PauseCanvas.enabled) {
				Time.timeScale = 1f;
				PauseCanvas.enabled = false;
			} else {
				Time.timeScale = 0f;
				PauseCanvas.enabled = true;
			}
		}
	}

	public void Exit(){
		SceneManager.LoadScene ("MainMenu");
	}
}
