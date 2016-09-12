using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Credits : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKey(KeyCode.Escape) || Input.GetButton("1B")){
			SceneManager.LoadScene("MainMenu");

		}
	}
}
