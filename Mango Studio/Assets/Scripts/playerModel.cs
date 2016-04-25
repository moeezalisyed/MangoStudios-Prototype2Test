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
	public List<bool> shadowSPAB = new List<bool> ();
	public Boolean firstRun = true;
	public int shadowitr = 0;

	public void init(int playerType,  Player owner) {
		this.owner = owner;
		this.playerType = playerType;
		movex = 0;
		movey = 0;
//		healthval = 40;
//		damagebuf = 0;




		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);
		name = "Player Model";
		mat = GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");

		if (playerType == 1) {
			//circle
		//	cd = owner.m.coolDownCircle;
			cdbuf = -0.5f;
			this.cdA = 1.5f;
			this.cdbufA = 0f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			//mat.color = Color.red;
		} else if (playerType == 0) {
			//square
			//cd = owner.m.coolDownCircle;
			cdbuf = -0.5f;
			this.cdA = 1.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/wdot");
			//mat.color = Color.red;
		} else if (playerType == 2) {
			//triangle 
		//	cd = 0;
			cdbuf = -0.5f;
			this.cdA = 1.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Triangle");
			//mat.color = Color.red;
		} else if (playerType == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");
			mat.color = new Color (1, 5, 1, 1);
			//transform.eulerAngles = new Vector3 (0, 0, -45);
		}

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
			if (Input.GetKeyDown (KeyCode.Space)) {
				shadowFiring.Add (true);
			} else {
				shadowFiring.Add (false);
			}
			if (Input.GetKeyDown (KeyCode.Z)) {
				shadowSPAB.Add (true);
			} else {
				shadowSPAB.Add (false);
			}
		
//			if (playerType == 2) {
////				if (movex > 0) {
////					transform.position = new Vector3 (transform.position.x + speed * Mathf.Sqrt (3) / 2, transform.position.y - speed / 2, 0);
////				} else if (movex < 0) {
////					transform.position = new Vector3 (transform.position.x - speed * Mathf.Sqrt (3) / 2, transform.position.y - speed / 2, 0);
////				} else if (movey > 0) {
////					transform.position = new Vector3 (transform.position.x, transform.position.y + speed, 0);
////				} else if (movey < 0) {
////					transform.position = new Vector3 (transform.position.x + speed / 2, transform.position.y - speed * Mathf.Sqrt (3) / 2, 0);
////				}
//			} else if (playerType == 1) {
//				transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
//			} else if (playerType == 3) {
			//	transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
//			}
//			if (clock - damagebuf > 3) {
//				damage ();
//				damagebuf = clock;
//			}
		} else {
			
			if (shadowitr >= shadowMovements.Count) {
				//shadowitr = 0;
				this.mat.color = Color.black;
				this.owner.tag = "Dead";
				this.transform.position = new Vector3(Screen.width +200, Screen.height + 200, shadowMovements[0].z);
				this.owner.GetComponent<BoxCollider2D> ().transform.position = transform.position;

			} else {
				
				this.owner.tag = "Player";
				this.mat.color = Color.gray;
				this.transform.eulerAngles = new Vector3 (0, 0, (this.shadowDirection[shadowitr]%4) * 90+(this.shadowDirection[shadowitr]/4)*45);
				//this.mat.shader = Shader.Find("Transparent/Diffuse");
				if (shadowFiring [shadowitr] == true) {
					this.shoot ();
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
				this.owner.m.whenPlayerDies ();
				this.destroy ();
			} else {
				shadowitr = this.shadowDirection.Count;

			}
		}
	}

	public void destroy(){
		
			this.firstRun = false;
			//this.owner.m.THEBOSS.bossHealth = 100;
			
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

	public void shoot(){
		
		if (clock - cdbuf > cd) {
			if (this.firstRun) {
				this.owner.m.PlayEffect (this.owner.m.shootClip);
			}

			GameObject bulletObject = new GameObject();		
			Bullet bullet = bulletObject.AddComponent<Bullet>();
			bullet.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,0);
			bullet.transform.rotation = new Quaternion(this.transform.rotation .x,this.transform.rotation.y,this.transform.rotation.z,this.transform.rotation.w);
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
			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string s = "";

			for (int i = 0; i < (cd - clock + cdbuf) * 10; i++) {
				s += "I";
			}


			GUI.Box (new Rect (490, 5, 200, 100), "Cooldown: \n" + s);

			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		

			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string p = "";

			for (int i = 0; i < ((owner.cdA - owner.clock + owner.cdbufA)  * 10); i++) {
				p += "I";
			}


			GUI.Box (new Rect (730, 5, 200, 100), "Special: \n" + p);

			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		
			if (this.owner.usingability) {
				GUI.color = Color.green;
				GUI.skin.box.fontSize = 22;
				GUI.Box (new Rect (Screen.width - 150, Screen.height - 150, 100, 100), "Ability on!");
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

		for (int i = 0; i < this.healthval; i++) {

			ss += "I";

		}

		Vector2 targetPos;
		targetPos = Camera.main.WorldToScreenPoint (transform.position);

		GUI.Box(new Rect(targetPos.x-30, Screen.height-targetPos.y-50, 60, 20), ss);
	}





}
