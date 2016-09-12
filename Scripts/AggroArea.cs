using UnityEngine;
using System.Collections;

public class AggroArea : MonoBehaviour {


	public GameObject enemy;
	public bool aggroenemy = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(enemy != null){
			transform.position = enemy.transform.position;
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player" && !aggroenemy && enemy != null){
			enemy.GetComponent<EnemyAI>().attacking = true;
			enemy.GetComponent<EnemyAI>().player = col.gameObject;
			aggroenemy = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col){
		if(col.gameObject.tag == "Player" && aggroenemy && enemy != null){
			enemy.GetComponent<EnemyAI>().attacking = false;
			enemy.GetComponent<EnemyAI>().player = null;
			aggroenemy = false;
		}
	}
}
