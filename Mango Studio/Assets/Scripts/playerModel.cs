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
			cd = 0;
			cdbuf = 2.0f;
			this.cdA = 1.5f;
			this.cdbufA = 0f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			mat.color = new Color (1, 1, 1, 1);
		} else if (playerType == 0) {
			//square
			cd = 0.0f;
			cdbuf = 0.7f;
			this.cdA = 1.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Square");
			mat.color = new Color (1, 1, 1, 1);
		} else if (playerType == 2) {
			//triangle 
			cd = 0;
			cdbuf = 0.5f;
			this.cdA = 1.5f;
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/Triangle");
			mat.color = new Color (5, 1, 1, 1);
		} else if (playerType == 3) {
			mat.mainTexture = Resources.Load<Texture2D> ("Textures/wdot");
			mat.color = new Color (1, 5, 1, 1);
			//transform.eulerAngles = new Vector3 (0, 0, -45);
		}

	}

	void Start(){
		clock = 0f;
		speed = 0.1f;
	}

	void Update(){


		// Kinda funky world wrapping - commented out for now!

//		Vector3 curPos = Camera.main.WorldToScreenPoint(this.transform.position);
//		curPos.x = curPos.x % Screen.width;
//		print ("player x : " + curPos.x + "width: " + Screen.width);
//		curPos.y = curPos.y % Screen.height;
//		curPos = Camera.main.ScreenToWorldPoint (curPos);
//		this.transform.position = curPos;

		owner.clock += Time.deltaTime;
		clock += Time.deltaTime;
		if (firstRun) {
			shadowMovements.Add (this.transform.position);
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
				transform.position = new Vector3 (transform.position.x + speed * movex, transform.position.y + speed * movey);
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

			} else {
				this.owner.tag = "Player";
				this.mat.color = Color.gray;
				this.transform.eulerAngles = new Vector3 (0, 0, this.shadowDirection[shadowitr] * 90);
				//this.mat.shader = Shader.Find("Transparent/Diffuse");
				if (shadowFiring [shadowitr] == true) {
					this.shoot ();
				}
				if (shadowSPAB [shadowitr] == true) {
					this.owner.useAbility ();
				}
				this.transform.position = shadowMovements [shadowitr];
				shadowitr++;
			}
		
		}
	}

	//void OnGUI(){
//		GUI.color = Color.green;
//		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
//		string s = "";
//		for (int i = 0; i < healthval / 10; i++) {
//			s += "I";
//		}
//		GUI.Box(new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 150, 100), s);
//		GUI.color = Color.white;
//		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
//
//		//the cd bar
//		GUI.color = Color.red;
//		GUI.skin.box.alignment = TextAnchor.MiddleLeft;
//		string t = "";
//		/*for (int i = 0; i < (int)(cd-clock+cdbuf)*100; i++) {
//			t += cd-clock+cdbuf;
//		}*/
//		t = String.Format("{0:0,0.0000000}", cd-clock+cdbuf);
//		GUI.Box(new Rect (Screen.width / 2 - 200, Screen.height / 2 + 200, 150, 100), t);
//		GUI.color = Color.white;
//		GUI.skin.box.alignment = TextAnchor.MiddleCenter;
	
	
	//}


	public void move(int x, int y){
		movex = x;
		movey = y;
	}

	public int getHealth(){
		return healthval;
	}

	public void damage(){
		healthval--;
		if (healthval == 0) {
			if (firstRun) {
				this.destroy ();
			} else {
				shadowitr = this.shadowDirection.Count;

			}
		}
	}

	public void destroy(){
		
			firstRun = false;
			this.owner.m.THEBOSS.bossHealth = 100;
			
			//this.healthval = 10;
			//this.owner.m.THEBOSS.model.transform.position = new Vector3(0,0, 0);
			foreach (Player x in owner.m.shadowPlayers) {
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
	}

//	void OnTriggerEnter2D(Collider2D other){
//		//print ("col");
//		if (other.name == "Boss") {
//			this.damage ();
//		}
//		if (other.name == "BossBullet") {
//			this.damage ();
//		}
//		if (other.name == "BossBeam") {
//			this.damage ();
//		}
//	}

	void OnGUI(){
		if (this.firstRun) {
			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string s = "";

			for (int i = 0; i < (cd - clock + cdbuf) * 10; i++) {
				s += "I";
			}


			GUI.Box (new Rect (10, 500, 200, 100), "Cooldown: \n" + s);

			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		

			GUI.color = Color.yellow;
			GUI.skin.box.alignment = TextAnchor.MiddleLeft;
			GUI.skin.box.fontSize = 25;
			string p = "";

			for (int i = 0; i < (owner.cdA - owner.clock + owner.cdbufA) * 10; i++) {
				p += "I";
			}


			GUI.Box (new Rect (10, 650, 200, 100), "Special: \n" + p);

			GUI.color = Color.white;
			GUI.skin.box.fontSize = 12;
			GUI.skin.box.alignment = TextAnchor.MiddleCenter;
		
		
		
		
		
		
		}
	}





}
