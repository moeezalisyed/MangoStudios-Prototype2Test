using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class playerModel : MonoBehaviour
{
	private float clock;	// to keep track of the time(not used for now)
	public Player owner;	// object that created it
	public Material mat;	// material (for texture)
	private int playerType;	// the type of the player(0, 1, 2)
	private int movex;
	private int movey;
	private float speed;
	public int healthval = 5;
	//private float damagebuf;
	public float cd;
	public float cdbuf;
	public	float cdbufA= 0f;		//time until ability cooldown is over
	public float cdA= 0f;	//cooldown length for ability



	public List<Vector3> shadowMovements = new List<Vector3>();
	public List<Boolean> shadowFiring =  new List<Boolean>();
	public List<int> shadowDirection =  new List<int>();
	public List<int> firingDirection =  new List<int>();
	public List<bool> shadowSPAB = new List<bool> ();
	public Boolean firstRun = true;
	public int shadowitr = 0;

	private Texture cooldownText;
	private Texture specialText;
	private Texture abilityOnText;

	public void init(int playerType,  Player owner) {
		this.owner = owner;
		this.playerType = playerType;
		movex = 0;
		movey = 0;
//		healthval = 40;
//		damagebuf = 0;

		cooldownText = Resources.Load<Texture2D> ("Textures/cooldown");
		specialText = Resources.Load<Texture2D> ("Textures/special");
		abilityOnText = Resources.Load<Texture2D> ("Textures/ability on");



		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);
		name = "Player Model";
		mat = GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");

		if (playerType == 1) {
			//circle
		//	cd = owner.m.coolDownCircle;
			cdbuf = -0.5f;
			this.cdA = 20.5f;
			this.cdbufA = 0f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			//mat.color = Color.red;
		} else if (playerType == 0) {
			//square
			//cd = owner.m.coolDownCircle;
			cdbuf = -0.5f;
			this.cdA = 20.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");
			//mat.color = Color.red;
		} else if (playerType == 2) {
			//triangle 
		//	cd = 0;
			cdbuf = -0.5f;
			this.cdA = 20.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/triangle2");
			transform.localScale = new Vector3 (1.5f, 1.5f, 0);	
			//mat.color = Color.red;
		} /*else if (playerType == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");
			mat.color = new Color (1, 5, 1, 1);
			//transform.eulerAngles = new Vector3 (0, 0, -45);
		}*/

	}

	void Start(){
		clock = 0f;
		speed = 0.1f;
	}

	void Update(){

		//this.owner.GetComponent<BoxCollider2D> ().size = transform.localScale;

		// Kinda funky world wrapping - commented out for now!

	//Vector3 curPos = Camera.main.WorldToScreenPoint(this.transform.position);
		//print ("player x : " + curPos.x + "width: " + Screen.width);
		//print ("player y : " + curPos.x + "player y: " + curPos.y);

		// Much improved handling of screen bounds - but still has some funly movement porblmes - will work on getting it done right
		/*
		//Handle Cornres
		// top left
		if (curPos.x <= 22 && curPos.y >= Screen.height - 22) {
			curPos.x = 22;
			curPos.y = Screen.height - 22;
		} 
		//print ("player x : " + curPos.x + "width: " + Screen.width);

		//top right
		else if (curPos.x >= Screen.width - 22 && curPos.y >= Screen.height - 22) {
			curPos.x = Screen.width - 22;
			curPos.y = Screen.height - 22;
		}

		//bottom right
		else if (curPos.x >= Screen.width - 22 && curPos.y <= 22) {
			curPos.x = Screen.width - 22;
			curPos.y = 22;
		}

		//bottom left
		else if (curPos.x <=  22 && curPos.y <= 22) {
			curPos.y =  22;
			curPos.x = 22;
		}

		//Handle the right and left
		else if (curPos.x >= Screen.width - 22) {
			curPos.x = Screen.width -22;		
		} else if (curPos.x <= 22) {
			curPos.x = 22;		
		}



		// Handle the top and Bottom
		else if (curPos.y >= Screen.height - 22) {
			curPos.y = Screen.height -22;		
		} else if (curPos.y <= 22) {
			curPos.y = 22;		
		}*/

		//curPos = Camera.main.ScreenToWorldPoint (curPos);
		//this.transform.position = curPos;

		owner.clock += Time.deltaTime;
		clock += Time.deltaTime;
		if (firstRun) {
			shadowMovements.Add (this.transform.position);
			this.owner.GetComponent<BoxCollider2D> ().transform.position = transform.position;
			if (Input.GetKeyDown (KeyCode.A)||Input.GetKeyDown (KeyCode.W)||Input.GetKeyDown (KeyCode.S)||Input.GetKeyDown (KeyCode.D)) {
				shadowFiring.Add (true);
			} else {
				shadowFiring.Add (false);
			}
			if (Input.GetKeyDown (KeyCode.Space)) {
				shadowSPAB.Add (true);
			} else {
				shadowSPAB.Add (false);
			}
		} else {
			
			if (shadowitr >= shadowMovements.Count) {
				//shadowitr = 0;
				this.mat.color = Color.black;
				this.owner.tag = "Dead";
				this.transform.position = new Vector3(Screen.width +200, Screen.height + 200, shadowMovements[0].z);
				this.owner.GetComponent<BoxCollider2D> ().transform.position = transform.position;

			} else {
				
				this.owner.tag = "Player";
				//this.mat.color = Color.grey;
				this.mat.color = new Color(0.3f, 0.3f, 0.3f, 1);
				this.transform.eulerAngles = new Vector3 (0, 0, (this.shadowDirection[shadowitr]%4) * 90+(this.shadowDirection[shadowitr]/4)*45);
				//this.mat.shader = Shader.Find("Transparent/Diffuse");
				if (shadowFiring [shadowitr] == true) {
					this.shoot (this.firingDirection[shadowitr]);
				}
				if (shadowSPAB [shadowitr] == true) {
					this.owner.useAbility ();
				}
				this.transform.position = shadowMovements [shadowitr];
				//***Jun Offset Code***
				this.owner.GetComponent<BoxCollider2D> ().transform.position = transform.position;

				shadowitr++;
			}
		
		}
	}




	public void move(int x, int y){
		movex = x;
		movey = y;
	}

	public int getHealth(){
		return healthval;
	}



	public void whenPlayerDiesAnum(){
		this.owner.m.PlayEffect (this.owner.m.playerDead);
		StartCoroutine (pdani());
	}

	IEnumerator pdani (){
		foreach (GameObject x in this.owner.m.bulletsFolder) {
			Destroy (x);
		}

		this.owner.m.bulletsFolder.Clear ();


		yield return new WaitForSeconds (3.05f);
		this.owner.m.whenPlayerDies ();
		this.destroy ();



	}



	public void damage(){
		if (firstRun) {
			healthval--;
			print ("Took damage in first run");
		} 

		if (!firstRun) {
			healthval--;
			print ("Took damage as shadow");
		}



		if (healthval <= 0) {
			if (firstRun) {
				if (healthval == 0 || this.owner.timeIndex == 0 ) {
					//this.mat.color = Color.black;
					this.owner.initDead (transform.position.x, transform.position.y, 4);
				}
				StopCoroutine (this.owner.usingabil ());
				this.owner.endLifeStopPowerUp ();
				this.whenPlayerDiesAnum();
				//StopCoroutine (this.owner.usingabil ());
				//this.owner.endLifeStopPowerUp ();
				//this.owner.m.whenPlayerDiesAnum();
				//this.destroy ();

			} else {
				shadowitr = this.shadowDirection.Count;

			}
		}
	}

	public void destroy(){
		
			this.firstRun = false;
			//this.owner.m.THEBOSS.bossHealth = 100;
			//this.mat.color = new Color(1,1,1,1);
			//this.healthval = 10;
			//this.owner.m.THEBOSS.model.transform.position = new Vector3(0,0, 0);
			foreach (Player x in owner.m.shadowPlayers) {
				x.tag = "Player";
				x.model.shadowitr = 0;
				x.model.healthval = 5;
				x.model.owner.playerbody.isTrigger = true;
			}
		 

	}

	public int getType(){
		return playerType;
	}

	public void shoot(int x){
		
		if (clock - cdbuf > cd) {
			if (this.firstRun) {
				this.owner.m.PlayEffect (this.owner.m.shootClip);
			}

			GameObject bulletObject = new GameObject();		
			Bullet bullet = bulletObject.AddComponent<Bullet>();
			bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
			bullet.transform.eulerAngles = new Vector3(0, 0, x);
			bullet.init (this);
			cdbuf = clock;
		}
	}
		

	public void setCD(float a){
		cd = a;
	}

	void OnBecameInvisible() {
		print ("went off screen");

		//We can handle it here as well
		//this.transform.position = new Vector3(0,0,0);
	}
		

	void OnGUI(){
		if (this.firstRun) {
			GUI.Box (new Rect (470, 28, 200, 33), cooldownText);
			GUI.Box (new Rect (730, 28, 200, 33), specialText);
			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			//string s = "";

//			for (int i = 0; i < (cd - clock + cdbuf) * 10; i++) {
//				s += "I";
//			}
			int index = (int) ((cd - clock + cdbuf)*10);
			if (index > 10) {
				index = 10;
			} 
			//print ("Cooldwon: " + index);
			if (index > 0) {
				//GUI.Box (new Rect (490, 28, 200, 33), cooldownText);
				GUI.Box (new Rect (470, 55, 200, 50), Resources.Load<Texture>("Textures/bar"+index));
			}
			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		

			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
//			for (int i = 0; i < ((owner.cdA - owner.clock + owner.cdbufA)  * 10); i++) {
//				p += "I";
//			}
			int index2 = (int)(((owner.cdA - owner.clock + owner.cdbufA)  * 10)/7);
			if (index2 > 10) {
				index2 = 10;
			} 


			if (index2 > 0) {
				//GUI.Box (new Rect (730, 28, 200, 33), specialText);
				GUI.Box (new Rect (730, 55, 200, 50), Resources.Load<Texture> ("Textures/bar" + index2));
			}

			int timeind1 = this.owner.timeIndex % 10;
			int timeind2 = (int) this.owner.timeIndex /10;

				//GUI.Box (new Rect (0, 55, 200, 50), Resources.Load<Texture>("Textures/bar"+timeind));
				//Make a GUI TEXTURE HERE ******JUN LI*******
				//*******CHANGE THE LOCATION AS WELL*******
			if (this.owner.timeIndex > this.owner.playerTimeOut * 0.3) {
				GUI.color = Color.green;
			} else {
				GUI.color = Color.red;
			}
			GUI.Box (new Rect (20, 55, 40, 50), Resources.Load<Texture>("Textures/number"+timeind2));
			GUI.Box (new Rect (60, 55, 40, 50), Resources.Load<Texture>("Textures/number"+timeind1));


			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		
			if (this.owner.usingability) {
				GUI.color = Color.green;
				GUI.skin.box.fontSize = 22;
				GUI.Box (new Rect (Screen.width - 150, Screen.height - 150, 100, 100), abilityOnText);
				GUI.skin.box.fontSize = 12;
				GUI.color = Color.white;
			}
		}

		if (this.healthval > 3) {			
			GUI.color = Color.green;
		} else {
			GUI.color = Color.red;
		}
		GUI.skin.box.alignment = TextAnchor.LowerLeft;
		GUI.skin.box.fontSize = 16;
		String ss = "";

//		for (int i = 0; i < this.healthval; i++) {
//
//			ss += "I";
//
//		}

		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);

		if (!this.firstRun) {
			GUI.color = new Color (0.3f, 0.3f, 0.3f, 1);
		}
		GUI.Box (new Rect (targetPos.x - 30, Screen.height - targetPos.y - 50, 60, 20), Resources.Load<Texture> ("Textures/bar" + this.healthval));
	}





}
