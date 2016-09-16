using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	public GameObject PlayerHP, PlayerHPBar; //PlayerHP is the container for HP text (example: 100/100). PlayerHpBar is the red bar that shrinks when the player takes damage. for EP is similar. 
	public GameObject PlayerEP, PlayerEPBar;
	public GameObject Player;
	string HP, EP;
	float HPBar, EPBar;
	public float MaxEP, MaxHP;
	
	// Use this for initialization
	void Start () {
		PlayerControl playerctrl = Player.GetComponent<PlayerControl>(); //Getting the PlayerControl script to work with HP and EP
		MaxEP = playerctrl.EnergyPoints;
		MaxHP = playerctrl.HealthPoints;

		if(MaxEP==0f){ //Making sure that the player UI doesn't start with 0f EP and HP. Those are the default values
			MaxEP = 30f;
		}
		if(MaxHP==0f){
			MaxHP = 100f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Player != null){
		
			PlayerControl playerctrl = Player.GetComponent<PlayerControl>(); //Getting the PlayerControl script to work with HP and EP
			HP = playerctrl.HealthPoints.ToString();
			EP = playerctrl.EnergyPoints.ToString();
			HPBar = (float)playerctrl.HealthPoints;
			HPBar = ((HPBar * 100)/ MaxHP) / 100;
			EPBar = (float)playerctrl.EnergyPoints;
			EPBar = ((EPBar * 100)/ MaxEP) / 100;

			PlayerHP.GetComponent<Text> ().text = "HP: " + HP + "/" + MaxHP.ToString();
			PlayerEP.GetComponent<Text> ().text = "EP: " + EP + "/" + MaxEP.ToString();
			PlayerHPBar.transform.localScale = new Vector3 (HPBar,PlayerHPBar.transform.localScale.y,PlayerHPBar.transform.localScale.z);
			PlayerEPBar.transform.localScale = new Vector3 (EPBar,PlayerEPBar.transform.localScale.y,PlayerEPBar.transform.localScale.z);
		}else{
			PlayerHP.GetComponent<Text> ().text = "HP: 000";
			PlayerEP.GetComponent<Text> ().text = "EP: 00";
		}
	
	}
}
