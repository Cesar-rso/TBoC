using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {

		StartCoroutine (Counter());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Counter(){
		yield return new WaitForSeconds(4.0f);
		GetComponent<SceneFadeInOut> ().EndScene ();
		//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
