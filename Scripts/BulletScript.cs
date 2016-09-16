using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	public float bulletspeed;
	public GameObject owner;
	Vector2 constantSpeed;
	// Use this for initialization
	void Start () {

		if(transform.tag=="Attack3"){
			if (owner.GetComponent<PlayerControl> ().lastDirection == "right") {
				transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletspeed, 0f);
				float xScale = transform.localScale.x;
				xScale *= -1;
				transform.localScale = new Vector3 (xScale, transform.localScale.y, transform.localScale.z);
			}
			if (owner.GetComponent<PlayerControl> ().lastDirection == "left") {
				transform.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-bulletspeed, 0f);
			}
		}

		constantSpeed = transform.gameObject.GetComponent<Rigidbody2D> ().velocity;

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.tag == "Attack3") {
			transform.gameObject.GetComponent<Rigidbody2D> ().velocity = constantSpeed;
		}
	}


	void OnTriggerEnter2D(Collider2D col) {
		if((!col.gameObject.tag.Contains("trigger") || (!col.gameObject.name.Contains("Spawner")) && !col.gameObject.name.Equals(owner.name))){
			Debug.Log (col.gameObject.tag + " " + col.gameObject.name);
			Destroy(transform.gameObject);
		}
	}

}
