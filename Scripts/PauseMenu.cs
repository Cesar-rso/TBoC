using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {


	public Canvas PauseCanvas, OptionsCanvas;
	public Slider VolumeSlider; 

	// Use this for initialization
	void Start () {
		if(PauseCanvas != null){
			PauseCanvas.enabled = false;
		}
		if(OptionsCanvas != null){
			OptionsCanvas.enabled = false;
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

	public void OptionsMenu(){

		if (OptionsCanvas.enabled && Time.timeScale==0f) {
			OptionsCanvas.enabled = false;
			PauseCanvas.enabled = true;

		} else if (Time.timeScale == 0f) {
			OptionsCanvas.enabled = true;
			PauseCanvas.enabled = false;

		}
	}
}
