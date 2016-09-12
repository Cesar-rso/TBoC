using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public string left_Control = "left";               //key bindings
	public string right_Control = "right";              //key bindings
	public string up_Control = "up";                  //key bindings
	public string down_Control = "down";                //key bindings
	public string atk1_Control = "RightControl";        //key bindings

	public string lastDirection = "";                  //last direction player faced
	public string bullettype = "arrow";               //identifying the type of projectile the player has
	public string weapontype = "";                    //type of melee weapon the player has
	public string PlayerID;
	public bool Default_Keys = true;                    //if player is using default key bindings
	public bool DoubleJumped = false;                   //if player already performed a double jump
	public bool onAir = false;                          //if player is in the air or grounded when jumping
	public bool MeleeAttacking = false;                //Is the player performing an melee attack?
	public bool hasWeapon = false;                     //Does the player have a melee weapon?
	public bool ControllerPlugged = false;             //Is the controller plugged?

	public int HealthPoints = 100;
	public int EnergyPoints = 30;
	
	public GameObject bulletpref;
	public GameObject bulletpref2;
	public GameObject swordpref;
	public GameObject staffpref;
	
	public Transform grounder;  //object attach to player's feet, to detect if it's grounded or not
	public LayerMask ground;   //layer mask for ground and platforms, to easily identify ground


	public Canvas PauseCanvas;
	
	public bool CounterManager = false;  //if counter already finished counting
	public bool lastShot = false;        //if has passed enough time since last shot
	
	// Use this for initialization
	void Start () {
		if(PauseCanvas != null){
			PauseCanvas.enabled = false;
		}
		if(Time.timeScale < 1f){
			Time.timeScale = 1f;
		}

		PlayerID = transform.name;
	}
	
	// Update is called once per frame
	void Update () {
	//
		transform.rotation = new Quaternion(0,0,0,0);
		if(Physics2D.OverlapCircle(grounder.transform.position, 0.5f, ground)){
			onAir = false;
			DoubleJumped = false;
		}else{
			onAir = true;
		}
		//Player actions
		if(Default_Keys){
			if(Input.GetKeyDown(KeyCode.D) && transform.GetComponent<Rigidbody2D>().velocity.x < 6.0f){
				//transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1200 * Time.deltaTime);
				transform.GetComponent<Rigidbody2D>().velocity = new Vector2(5f,transform.GetComponent<Rigidbody2D>().velocity.y);
				lastDirection = "right";
			}
			if(Input.GetKeyUp(KeyCode.D)){
				transform.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f,transform.GetComponent<Rigidbody2D>().velocity.y);
			}
			if(Input.GetKeyUp(KeyCode.A)){
				transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f,transform.GetComponent<Rigidbody2D>().velocity.y);
			}
			if(Input.GetKeyDown(KeyCode.A) && transform.GetComponent<Rigidbody2D>().velocity.x > -6.0f){
				//transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -1200 * Time.deltaTime);
				transform.GetComponent<Rigidbody2D>().velocity = new Vector2(-5f,transform.GetComponent<Rigidbody2D>().velocity.y);
				lastDirection = "left";
			}
			if(Input.GetKeyDown(KeyCode.W) && DoubleJumped == false){
				transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 32000 * Time.deltaTime);
				
				if(onAir){
					DoubleJumped = true;
				}
			}
			if(Input.GetKeyDown(KeyCode.S)){
				transform.localScale -= new Vector3(0, 0.3f, 0);
			}
			if(Input.GetKeyUp(KeyCode.S)){
				transform.localScale += new Vector3(0, 0.3f, 0);
			}
			if(Input.GetKey(KeyCode.K)){
				Attack1();
			}
			if(Input.GetKeyDown(KeyCode.L)){
				Attack2();
			}
			if(Input.GetKey(KeyCode.I)){
				Attack3();
			}
			if(Input.GetKey(KeyCode.J) && !MeleeAttacking){
				DropWeapon ();
			}
			if(Input.GetKeyDown(KeyCode.Escape)){
				Pause();
			}
		}else{
			if(Input.GetKey(right_Control) && transform.GetComponent<Rigidbody2D>().velocity.x < 6.0f){
				PlayerMovement ("Horizontal", 1f);
			}
			if(Input.GetKey(left_Control) && transform.GetComponent<Rigidbody2D>().velocity.x > -6.0f){
				PlayerMovement ("Horizontal", -1f);
			}
			if(Input.GetKeyDown(up_Control) && transform.GetComponent<Rigidbody2D>().velocity.y < 6.0f && DoubleJumped == false){
				PlayerMovement ("Vertical", 1f);
			}
			if(Input.GetKeyDown(down_Control)){
				//transform.localScale -= new Vector3(0, 0.3f, 0);
				PlayerMovement ("Vertical", -1f);
			}
			if(Input.GetKeyUp(down_Control)){
				transform.localScale += new Vector3(0, 0.3f, 0);
			}
			if(Input.GetKey(KeyCode.RightControl)){
				Attack1();
			}
			if(Input.GetKeyDown(KeyCode.Return)){
				Attack2();
			}
			if(Input.GetKey(KeyCode.RightShift)){
				Attack3 ();
			}
			if(Input.GetKey(KeyCode.RightAlt) && !MeleeAttacking){
				DropWeapon ();
			}
			if(Input.GetKeyDown(KeyCode.Backspace)){
				Pause();
			}
		}

		if (ControllerPlugged) { //XBox 360 controller support
			if (Input.GetAxis (PlayerID.Substring (7, 1) + "D-Pad Horizontal") > 0f || Input.GetAxis (PlayerID.Substring (7, 1) + "D-Pad Horizontal") < 0f) { // side movement (left and right)
				PlayerMovement ("Horizontal", Input.GetAxis (PlayerID.Substring (7, 1) + "D-Pad Horizontal"));
			}
			float verticalDirection = Input.GetAxis (PlayerID.Substring (7, 1) + "D-Pad Vertical");
			if ((verticalDirection > 0f || verticalDirection < 0f) && !DoubleJumped) { //jump for positive values, crouch for negative
				if(onAir){
					DoubleJumped = true;
				}
				PlayerMovement ("Vertical", verticalDirection);
				Debug.Log (onAir + " " + DoubleJumped);
			}
			if (verticalDirection == 0f && transform.localScale.y < 0.7f && Time.timeScale > 0f) { //if player crouched, get up
				PlayerMovement ("Vertical", verticalDirection);
			}
			if (Input.GetButton (PlayerID.Substring (7, 1) + "A")) {
				Attack1 ();
			}
			if (Input.GetButton (PlayerID.Substring (7, 1) + "B") && !MeleeAttacking) {
				DropWeapon ();
			}
			if (Input.GetButton (PlayerID.Substring (7, 1) + "X")) {
				Attack2 ();
			}
			if (Input.GetButton (PlayerID.Substring (7, 1) + "Y")) {
				Attack3 ();
			}
			if (Input.GetButton (PlayerID.Substring (7, 1) + "Start")) {
				Pause ();
			}
		}
		if(!ControllerPlugged && Input.GetButton (PlayerID.Substring (7, 1) + "Start")){ //Detecting if the controller have been replugged
			ControllerPlugged = true;
			Debug.Log (transform.name);
		}


		if(Input.GetKey(KeyCode.X)){ //hard restart for debug purpose
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
		//Player energy points manager
		//if(EnergyPoints < 30 && CounterManager==false){
		//	CounterManager = true;
		//	EnergyPoints++;
		//	StartCoroutine(Counter());
		//}
		
		if(HealthPoints <= 0){ //if player is killed
			Destroy(transform.gameObject);
		}
	//Player Velocity limitation
		transform.GetComponent<Rigidbody2D>().angularVelocity = 0;
		transform.rotation.Set(0,0,0,0);
		if(GetComponent<Rigidbody2D>().velocity.x > 6.0f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(6.0f,transform.GetComponent<Rigidbody2D>().velocity.y);
		}
		if(GetComponent<Rigidbody2D>().velocity.x < -6.0f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(-6.0f,transform.GetComponent<Rigidbody2D>().velocity.y);
		}
		if(GetComponent<Rigidbody2D>().velocity.y > 6.0f){
			GetComponent<Rigidbody2D>().velocity = new Vector2(transform.GetComponent<Rigidbody2D>().velocity.x,6.0f);
		}
	}

	void PlayerMovement(string movetype, float direction){
		if (movetype.Equals ("Horizontal")) {
			transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * direction * 1700 * Time.deltaTime);
			if (direction > 0) {
				lastDirection = "right";
			} else {
				lastDirection = "left";
			}
		}
		if (movetype.Equals ("Vertical")) {
			if (direction > 0) {
				transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 32000 * Time.deltaTime);
			}
			if(direction < 0 && transform.localScale.y > 0.4f){
				transform.localScale -= new Vector3(0, 0.3f, 0);
			}
			if (direction == 0 && Time.timeScale > 0f) {
				transform.localScale += new Vector3(0, 0.3f, 0);
			}
				
		}
	}
	
	void Attack1 (){ //Arrow attacks
		if(EnergyPoints>0){
			Vector3 bposition = Vector3.zero;
			if(lastDirection == "right" || lastDirection == ""){
				bposition = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
			}
			if(lastDirection == "left"){
				bposition = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
			}
			//Bullet shot
			if(!lastShot){
				GameObject bullet = (GameObject)Instantiate(bulletpref, bposition, transform.rotation);
				lastShot = true;
				if(lastDirection=="right" || lastDirection==""){
					//bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 24000 * Time.deltaTime);
					bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Vector2.right.x * 10f, 4f);
				}else if(lastDirection=="left"){
					bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Vector2.left.x * 10f, 4f);
				}
				bullet.GetComponent<BulletScript> ().owner = transform.gameObject;
				EnergyPoints--;
				StartCoroutine(BulletCounter());
			}
		}
	}
	
	void Attack2 (){ // Melee attacks
		Vector3 sposition = Vector3.zero;
		//Debug.Log ("weapon: " + weapontype);
		if(weapontype == "sword"){
			
			Quaternion Srotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 40);
			if(lastDirection == "right" || lastDirection == ""){
				sposition = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
			}
			if(lastDirection == "left"){
				sposition = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
				Srotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 140);
			}
			GameObject sword = null;
			if(!MeleeAttacking){
				if (sword == null) {
					sword = (GameObject)Instantiate (swordpref, sposition, Srotation);
					sword.GetComponent<WeaponScript> ().player = transform.gameObject;
					sword.GetComponent<WeaponScript> ().WeaponGotten = true;
					sword.GetComponent<WeaponScript> ().weaponOwner = transform.name;
				}
				MeleeAttacking = true;
				StartCoroutine(SwordCounter());

			}

			sword = null;
		}
		if(weapontype == "staff"){
			if(lastDirection == "right" || lastDirection == ""){
				sposition = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
			}
			if(lastDirection == "left"){
				sposition = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
			}
			GameObject staff = null;
			if(!MeleeAttacking){
				if (staff == null) {
					staff = (GameObject)Instantiate (staffpref, sposition, transform.rotation);
					staff.GetComponent<WeaponScript> ().player = transform.gameObject;
					staff.GetComponent<WeaponScript> ().WeaponGotten = true;
					staff.GetComponent<WeaponScript> ().weaponOwner = transform.name;
				}
				MeleeAttacking = true;
				StartCoroutine(SwordCounter());
			}

			staff = null;
		}
		
	}

	void Attack3 (){ //Bullet attacks
		if(EnergyPoints>0){
			Vector3 bposition = Vector3.zero;
			if(lastDirection == "right" || lastDirection == ""){
				bposition = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
			}
			if(lastDirection == "left"){
				bposition = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.5f, transform.position.z);
			}
			//Bullet shot
			if(!lastShot){
				GameObject bullet = (GameObject)Instantiate(bulletpref2, bposition, transform.rotation);
				lastShot = true;
				bullet.GetComponent<BulletScript> ().bulletspeed = 6f;
				bullet.GetComponent<BulletScript> ().owner = transform.gameObject;
				EnergyPoints--;
				StartCoroutine(BulletCounter());
			}
		}
	}

	void DropWeapon(){
		if (hasWeapon) {
			hasWeapon = false;
			if (weapontype == "sword") {
				if (lastDirection == "right") {
					GameObject weaponItem = (GameObject)Instantiate (swordpref, new Vector3 (transform.position.x - 2.5f, transform.position.y, transform.position.z), transform.rotation);
					Destroy (weaponItem.GetComponent<Animator>());
					weaponItem.AddComponent<Rigidbody2D> ();
					weaponItem.GetComponent<BoxCollider2D> ().isTrigger = false;
					weaponItem.tag = "item";
				}
				if (lastDirection == "left") {
					GameObject weaponItem = (GameObject)Instantiate (swordpref, new Vector3 (transform.position.x + 1.5f, transform.position.y, transform.position.z), transform.rotation);
					Destroy (weaponItem.GetComponent<Animator>());
					weaponItem.AddComponent<Rigidbody2D> ();
					weaponItem.GetComponent<BoxCollider2D> ().isTrigger = false;
					weaponItem.tag = "item";
				}
			}
			if (weapontype == "staff") {
				if (lastDirection == "right") {
					GameObject weaponItem = (GameObject)Instantiate (staffpref, new Vector3 (transform.position.x - 2, transform.position.y, transform.position.z), transform.rotation);
					Destroy (weaponItem.GetComponent<Animator>());
					weaponItem.AddComponent<Rigidbody2D> ();
					weaponItem.GetComponent<BoxCollider2D> ().isTrigger = false;
					weaponItem.tag = "item";
				}
				if (lastDirection == "left") {
					GameObject weaponItem = (GameObject)Instantiate (staffpref, new Vector3 (transform.position.x + 2, transform.position.y, transform.position.z), transform.rotation);
					Destroy (weaponItem.GetComponent<Animator>());
					weaponItem.AddComponent<Rigidbody2D> ();
					weaponItem.GetComponent<BoxCollider2D> ().isTrigger = false;
					weaponItem.tag = "item";
				}
			}
			weapontype = "";
		}
	}

	public void Pause (){
		if (PauseCanvas != null) {
			if (PauseCanvas.enabled) {
				Time.timeScale = 1f;
				PauseCanvas.enabled = false;
			} else {
				Time.timeScale = 0f;
				PauseCanvas.enabled = true;
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Attack"){
			HealthPoints = HealthPoints - 2;
		}
		
		if(col.gameObject.name == "BottomFloor"){
			transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 12000 * Time.deltaTime);
		}
		if(col.gameObject.tag == "item"){
			if(col.gameObject.name.Contains("HPpotion")){
				HealthPoints = HealthPoints + 30;
				Destroy(col.gameObject);
			}
			if(col.gameObject.name.Contains("EPpotion")){
				EnergyPoints = EnergyPoints + 15;
				Destroy(col.gameObject);
			}
			if(col.gameObject.name.Contains("Sword") && !hasWeapon){
				weapontype = "sword";
				hasWeapon = true;
				Destroy (col.gameObject);
			}
			if(col.gameObject.name.Contains("Staff") && !hasWeapon){
				weapontype = "staff";
				hasWeapon = true;
				Destroy (col.gameObject);
			}
		}
		
	}
	
	
	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.tag == "Attack"){
			if (col.transform.name.Contains ("Staff") && col.transform.GetComponent<WeaponScript> ().weaponOwner != transform.name) {
				HealthPoints = HealthPoints - 2;
			}
			if (!col.transform.name.Contains ("Staff")) {
				HealthPoints = HealthPoints - 2;
			}
		}
		if(col.gameObject.tag == "Attack2" && col.transform.GetComponent<WeaponScript>().weaponOwner != transform.name){
			HealthPoints = HealthPoints - 6;
		}
		if(col.gameObject.tag == "Attack3"){
			HealthPoints = HealthPoints - 3;
		}
		if(col.gameObject.tag == "enemy"){
			transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6000 * Time.deltaTime);
			HealthPoints = HealthPoints - 2;
		}
		if(col.gameObject.tag == "item"){
			if(col.gameObject.name.Contains("HPpotion")){
				HealthPoints = HealthPoints + 30;
				Destroy(col.gameObject);
			}
			if(col.gameObject.name.Contains("EPpotion")){
				EnergyPoints = EnergyPoints + 15;
				Destroy(col.gameObject);
			}
			if(col.gameObject.name.Contains("Sword") && !hasWeapon){
				weapontype = "sword";
				hasWeapon = true;
				Destroy (col.gameObject);
			}
			if(col.gameObject.name.Contains("Staff") && !hasWeapon){
				weapontype = "staff";
				hasWeapon = true;
				Destroy (col.gameObject);
			}
		}
		
	}
	
	IEnumerator Counter(){
		yield return new WaitForSeconds(0.5f);
		CounterManager = false;
	}
	
	IEnumerator BulletCounter(){ //time between projectile shots
		yield return new WaitForSeconds(0.2f);
		lastShot = false;
	}
	
	IEnumerator SwordCounter(){ //time between melee attacks
		yield return new WaitForSeconds(1.8f);
		MeleeAttacking = false;
	}
}
