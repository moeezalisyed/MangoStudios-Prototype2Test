
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	public playerModel model;		// The model object.
	public int playerType;
	//private int initHealth;
	public GameManager m;		// A pointer to the manager (not needed here, but potentially useful in general).
	public int direction = 0;
	public BoxCollider2D playerbody;
	public bool usingability = false;
	public bool usingcircpowerup = false;
	public	float cdbufA= -0.5f;		//time until ability cooldown is over
	public float cdA= 0f;	//cooldown length for ability
	public float clock;	// to keep track of the time(not used for now)
	private float damageclock = .7f;
	private int playerTimeOut = 50;

	public void init(int playerType, GameManager m) {

		this.playerType = playerType;
		//this.initHealth = initHealth;
		this.m = m;


		if (this.playerType == 0){
			//triangle

			this.cdA = 1.5f;


		} else if (this.playerType == 1){
			//circle

			this.cdA = 1.5f;

		} else if (this.playerType == 2){
			//square

			this.cdA = 1.5f;

		}

//		camera = GetComponent<Camera>();
//		camera.clearFlags = CameraClearFlags.SolidColor;
//

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		playerbody = gameObject.AddComponent<BoxCollider2D> ();

		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<playerModel>();						// Add an playerModel script to control visuals of the gem.
		model.init(playerType, this);		
		this.tag = "Player";
		transform.localScale = new Vector3 (0.75f, 0.75f, 1);
		if (this.playerType == 1) {
			transform.localScale = new Vector3 (1.4f, 1f, 1);

		}
		StartCoroutine (this.playerTimer (this.playerTimeOut));
	}
	void Start(){
		clock = 0f;
	
	}

	public void move(int x, int y){
		this.model.transform.eulerAngles = new Vector3 (0, 0, 90 * this.direction);
		model.move (x, y);
	}

	public int getHealth(){
		return model.getHealth();
	}

	public void useAbility(){
		if (clock - cdbufA > cdA) {
			if (this.model.firstRun) {
				m.PlayEffect (this.m.abilityon);
			}
			StartCoroutine (usingabil ());
			cdbufA = clock;
		}

	}


	IEnumerator startPowerUp (){
		this.usingcircpowerup = true;
		yield return new WaitForSeconds (5);
		this.usingcircpowerup = false;
	}

	//Making the player temporary flash when it got hit
	IEnumerator whenGotHit (){
		//this.model.mat.color = Color.red;
		Camera.main.backgroundColor = Color.red;
		yield return new WaitForSeconds (0.03f);
		//this.model.mat.color = new Color(1,1,1,1);
		Camera.main.backgroundColor = Color.black;
	}

	IEnumerator usingabil (){
		this.usingability = true;
		if (this.playerType == 2) {
			this.setCD (this.model.cd/1.7f);
		}

		if (this.playerType == 1) {
			this.tag = "inviscircle";
			//print ("changed tag to " + this.tag);
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/CircleSP");
			transform.localScale = new Vector3 (3f, 3f, 1f);	
		} else if (this.playerType == 0) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/SquareSP");
		
		
		} else if (this.playerType == 2) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/triangle2SP");
//			transform.localScale = new Vector3 (1.5f, 15f, 0);	
		
		}
		yield return new WaitForSeconds (5);
		this.usingability = false;
		if (this.playerType == 2) {
			this.setCD (this.model.cd * 1.7f);
		}
		if (this.playerType == 1) {
			this.tag = "Player";
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			transform.localScale = new Vector3 (1f, 1f, 1f);
			transform.localScale = new Vector3 (1.4f, 1f, 1f);
		}else if (this.playerType == 0) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");


		} else if (this.playerType == 2) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/triangle2");
//			transform.localScale = new Vector3 (1.5f, 1.5f, 0);	

		}
	}

	public void damage(){
		model.damage ();
	}

	public void destroy(){
		model.damage();
	}


	public void shoot(int x){
		model.shoot (x);
	}

	public void setCD(float a){
		model.setCD (a);
	}

	public int getType(){
		return model.getType();
	}

	public float getX(){
		return transform.position.x;
	}

	public float getY(){
		return transform.position.y;
	}

	IEnumerator playerTimer (int secs){
	yield return new WaitForSeconds (secs);
		this.model.healthval -= 1000;
		this.model.damage ();

	}




	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "inviscircle") {
			//Here it will check if it collided with a circle
			/*if (other.GetComponentInParent (Texture2D) == "inviscircle") {
				//increase the damage stuff
				print("got to the invis circle");
			}*/

			print ("Got a powerup!");
			StartCoroutine (startPowerUp ());

		}




		if (other.name == "Boss") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				this.destroy ();
			}

		}
		if (other.name == "BossBullet") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				StartCoroutine (this.whenGotHit ());
				this.destroy ();
			}

		}
		if (other.name == "BossBeam") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				StartCoroutine (this.whenGotHit ());
				this.destroy ();
			}

		}
		if (other.name == "BossBlade") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				StartCoroutine (this.whenGotHit ());
				this.destroy ();
			}

		}
		if (other.name == "TracerBullet") {
			if (this.playerType == 0 && this.usingability) {
				// Square is invulnerable
			} else {
				StartCoroutine (this.whenGotHit ());
				this.destroy ();
			}

		}


	}

	void OnTriggerStay2D(Collider2D other){
		if (other.name == "AOE") {
			if (this.playerType == 0 && this.usingability) {
				// Square is invulnerable
			} else {
				if (damageclock <= 0) {
					damageclock = .7f;
					StartCoroutine (this.whenGotHit ());
					this.destroy ();
				} else {
					damageclock = damageclock - Time.deltaTime;
				}
			}
		}
	}


}

