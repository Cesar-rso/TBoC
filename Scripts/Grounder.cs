using UnityEngine;
using System.Collections;

public class Grounder : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col) {

		if (!col.gameObject.name.Equals (transform.GetComponentInParent<PlayerControl> ().PlayerID) && col.gameObject.tag == "Player") {
			transform.GetComponentInParent<PlayerControl> ().onAir = false;
			transform.GetComponentInParent<PlayerControl> ().DoubleJumped = false;
		}
	}
}
