using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public Slider VolumeSlider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		ChangeVolumeAll ();
	}

	public void ChangeVolumeAll(){

		AudioListener.volume = VolumeSlider.value;
	}
}
