using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour {


	public Canvas PauseCanvas;
	public GameObject ResumeButton, RestartButton, OptionsButton, QuitButton, VolumeSlider, LanguageMenu,BackButton;
	public GameObject datamanager;
	public int currentPauseMenu = 1;

	// Use this for initialization
	void Start () {
		VolumeSlider.GetComponent<AudioManager> ().VolumeSlider.value = datamanager.GetComponent<DataManager> ().volume;

		VolumeSlider.SetActive (false);
		LanguageMenu.SetActive (false);
		BackButton.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {

		if(!PauseCanvas.enabled && currentPauseMenu==2){
			currentPauseMenu = 1;

			ResumeButton.SetActive (true);
			RestartButton.SetActive (true);
			OptionsButton.SetActive (true);
			QuitButton.SetActive (true);

			VolumeSlider.SetActive (false);
			LanguageMenu.SetActive (false);
			BackButton.SetActive (false);
		}
	
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

	public void RestartLevel(){
		datamanager.GetComponent<DataManager> ().Save ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void Exit(){
		datamanager.GetComponent<DataManager> ().Save ();
		SceneManager.LoadScene ("MainMenu");
	}

	public void OptionsMenu(){

		if (currentPauseMenu == 1) {
			currentPauseMenu = 2;

			ResumeButton.SetActive (false);
			RestartButton.SetActive (false);
			OptionsButton.SetActive (false);
			QuitButton.SetActive (false);

			VolumeSlider.SetActive (true);
			LanguageMenu.SetActive (true);
			BackButton.SetActive (true);
			//Camera.main.GetComponent<AudioManager> ().VolumeSlider = volume;
		}
		else if(currentPauseMenu == 2){
			currentPauseMenu = 1;

			ResumeButton.SetActive (true);
			RestartButton.SetActive (true);
			OptionsButton.SetActive (true);
			QuitButton.SetActive (true);

			VolumeSlider.SetActive (false);
			LanguageMenu.SetActive (false);
			BackButton.SetActive (false);
		}

	}

	public void ChangeLanguage(){
	
		string newLanguage = LanguageMenu.GetComponent<Dropdown> ().captionText.text;
		Debug.Log (newLanguage);
		datamanager.GetComponent<DataManager> ().language = newLanguage;  
	}
}
