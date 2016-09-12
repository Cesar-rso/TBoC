using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public GameObject leftpoint, rightpoint, HPpref, EPpref, item = null, spawner, player = null;
	public bool movement = true, attacking = false;
	public int speed = 2, hp=20;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(!attacking){
			if(movement){
				transform.position = Vector3.MoveTowards(transform.position,leftpoint.transform.position,speed*Time.deltaTime);
			}else{
				transform.position = Vector3.MoveTowards(transform.position,rightpoint.transform.position,speed*Time.deltaTime);
			}
		
			if(transform.position.x>=rightpoint.transform.position.x){
				movement = true;
			} else if(transform.position.x<=leftpoint.transform.position.x){
				movement = false;
			}
			
			if(leftpoint.transform.position.y != transform.position.y){
				leftpoint.transform.position = new Vector3(leftpoint.transform.position.x, transform.position.y, transform.position.z);
				
				rightpoint.transform.position = new Vector3(rightpoint.transform.position.x, transform.position.y, transform.position.z);
			}
		}else{
			if (player != null) {
				transform.position = Vector3.MoveTowards (transform.position, new Vector3 (player.transform.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
			
				float posX = transform.position.x - 1f;
				leftpoint.transform.position = new Vector3 (posX, transform.position.y, transform.position.z);
			
				rightpoint.transform.position = new Vector3 (posX + 2f, transform.position.y, transform.position.z);
			}
		}
		
		if(hp <= 0){ //if enemy is killed
			float chance = Random.Range(0.0f,10.0f);
			if(chance <= 4.0f){
				chance = Random.Range(0.0f,10.0f);
				if(chance <= 5.0f){
					item = (GameObject)Instantiate(HPpref, transform.position, transform.rotation);
				}else{
					item = (GameObject)Instantiate(EPpref, transform.position, transform.rotation);
				}
			}
			spawner.GetComponent<SpawnerScript>().DeadEnemy();
			Destroy(transform.gameObject);
		}
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Attack" && !col.gameObject.name.Contains("BottomFloor")){
			hp = hp - 2;
		}
		if(col.gameObject.tag == "Attack2"){
			hp = hp - 6;
		}
		
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Attack" && !col.gameObject.name.Contains("BottomFloor")){
			hp = hp - 2;
		}
		if(col.gameObject.tag == "Attack2"){
			hp = hp - 6;
		}
	}
}
