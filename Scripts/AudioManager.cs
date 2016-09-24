using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public Slider VolumeSlider;
	public GameObject datamanager;

	// Use this for initialization
	void Start () {
		if (VolumeSlider != null) {
			AudioListener.volume = VolumeSlider.value;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		//ChangeVolumeAll ();
	}

	public void ChangeVolumeAll(){

		if (VolumeSlider != null) {
			AudioListener.volume = VolumeSlider.value;
			datamanager.GetComponent<DataManager> ().volume = VolumeSlider.value;
		}
	}
}
