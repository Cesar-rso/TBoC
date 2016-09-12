using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public GameObject timeObj;
	int seconds = 0, minutes = 0;
	string InstantTime, strMinutes, strSeconds;
	bool lockedTimer = false;
	
	// Update is called once per frame
	void Update () {
		if(!lockedTimer){
			StartCoroutine (Counter());
			lockedTimer = true;
		}
	}

	IEnumerator Counter(){

		yield return new WaitForSeconds (1);
		InstantTime = timeObj.GetComponent<Text> ().text.ToString ();
		seconds = int.Parse (InstantTime.Substring(3,2));
		minutes = int.Parse (InstantTime.Substring(0,2));
		if (seconds == 59) {
			seconds = 0;
			minutes++;
		} else {
			seconds++;
		}
		strSeconds = seconds.ToString ("D2");
		strMinutes = minutes.ToString ("D2");
		timeObj.GetComponent<Text> ().text = strMinutes + ":" + strSeconds;
		lockedTimer = false;
	}
}
