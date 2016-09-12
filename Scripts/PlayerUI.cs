using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	public GameObject PlayerHP, PlayerHPBar;
	public GameObject PlayerEP, PlayerEPBar;
	public GameObject Player;
	string HP, EP;
	float HPBar, EPBar;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Player != null){
		
			PlayerControl playerctrl = Player.GetComponent<PlayerControl>();
			HP = playerctrl.HealthPoints.ToString();
			EP = playerctrl.EnergyPoints.ToString();
			HPBar = (float)playerctrl.HealthPoints;
			HPBar = HPBar / 100;
			EPBar = (float)playerctrl.EnergyPoints;
			EPBar = ((EPBar * 100) / 30) / 100;

			PlayerHP.GetComponent<Text> ().text = "HP: " + HP;
			PlayerEP.GetComponent<Text> ().text = "EP: " + EP;
			PlayerHPBar.transform.localScale = new Vector3 (HPBar,PlayerHPBar.transform.localScale.y,PlayerHPBar.transform.localScale.z);
			PlayerEPBar.transform.localScale = new Vector3 (EPBar,PlayerEPBar.transform.localScale.y,PlayerEPBar.transform.localScale.z);
		}else{
			PlayerHP.GetComponent<Text> ().text = "HP: 000";
			PlayerEP.GetComponent<Text> ().text = "EP: 00";
		}
	
	}
}
