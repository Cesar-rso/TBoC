using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {


	public bool countManager = false, WeaponGotten = false;
	public GameObject player;
	public string weaponOwner = "";

	// Use this for initialization
	void Start () {
		if(player != null && player.GetComponent<PlayerControl>().lastDirection == "left"){
			float xScale = transform.localScale.x;
			xScale *= -1;
			transform.localScale = new Vector3 (xScale, transform.localScale.y, transform.localScale.z);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(WeaponGotten){
			if(player.GetComponent<PlayerControl>().lastDirection == "right"){
				transform.position = new Vector3 (player.transform.position.x + 0.5f, player.transform.position.y, player.transform.position.z);
			}else if(player.GetComponent<PlayerControl>().lastDirection == "left"){
				transform.position = new Vector3 (player.transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
			}

			if(transform.tag=="Attack2" || (transform.tag=="Attack" && transform.name.Contains("Staff"))){

				StartCoroutine(Counter(1.2f));
				if(countManager){
					Destroy(transform.gameObject);
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col) {
	}

	IEnumerator Counter(float seconds){
		yield return new WaitForSeconds(seconds);
		countManager = true;
	}
}
