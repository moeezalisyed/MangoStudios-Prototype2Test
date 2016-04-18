
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

	public void init(int playerType, GameManager m) {
		this.playerType = playerType;
		//this.initHealth = initHealth;
		this.m = m;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D playerRbody = gameObject.AddComponent<Rigidbody2D> ();
		playerRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		model = modelObject.AddComponent<playerModel>();						// Add an playerModel script to control visuals of the gem.
		model.init(playerType, this);		
		this.tag = "Player";
	}

	public void move(int x, int y){
		this.model.transform.eulerAngles = new Vector3 (0, 0, 90 * this.direction);
		model.move (x, y);
	}

	public int getHealth(){
		return model.getHealth();
	}

	public void useAbility(){
		StartCoroutine (usingabil ());
	}

	IEnumerator usingabil (){
		this.usingability = true;
		if (this.playerType == 2) {
			model.cdbuf = 0.1f;
		}
		if (this.playerType == 1) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/inviscircle");
			transform.localScale = new Vector3 (2, 2, 0);
		}
		yield return new WaitForSeconds (3);
		this.usingability = false;
		if (this.playerType == 2) {
			model.cdbuf = 0.5f;
		}
		if (this.playerType == 1) {
			model.mat.mainTexture = Resources.Load<Texture2D> ("Textures/Circle");
			transform.localScale = new Vector3 (1, 1, 0);
		}
	}

	public void damage(){
		model.damage ();
	}

	public void destroy(){
		model.damage();
	}


	public void shoot(){
		model.shoot ();
	}

	public void setCD(float a){
		model.setCD (a);
	}

	public int getType(){
		return model.getType();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Circle") {
			//Here it will check if it collided with a circle
			/*if (other.GetComponentInParent (Texture2D) == "inviscircle") {
				//increase the damage stuff
				print("got to the invis circle");
			}*/
		
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
				this.destroy ();
			}

		}
		if (other.name == "BossBeam") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				this.destroy ();
			}

		}
		if (other.name == "BossBlade") {
			if (this.playerType == 0 && this.usingability) {
			// Square is invulnerable
			} else {
				this.destroy ();
			}

		}


	}


}

