using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

	public bool isEnemyDead = false, countManager = false;
	public GameObject enemypref, leftpoint, rightpoint, aggroarea;
	GameObject newEnemy;
	
	// Use this for initialization
	void Start () {
		newEnemy = null;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isEnemyDead){
			StartCoroutine(Counter());
			if(countManager){
				newEnemy = (GameObject)Instantiate(enemypref, transform.position, transform.rotation);
				newEnemy.GetComponent<EnemyAI>().leftpoint = leftpoint;
				newEnemy.GetComponent<EnemyAI>().rightpoint = rightpoint;
				newEnemy.GetComponent<EnemyAI>().spawner = transform.gameObject;
				aggroarea.GetComponent<AggroArea>().enemy = newEnemy;
				aggroarea.GetComponent<AggroArea>().aggroenemy = false;
				countManager = false;
				isEnemyDead = false;
			}
		}
	
	}
	
	public void DeadEnemy(){
		isEnemyDead = true;
	}
	
	IEnumerator Counter(){
		yield return new WaitForSeconds(3.0f);
		countManager = true;
	}
}
