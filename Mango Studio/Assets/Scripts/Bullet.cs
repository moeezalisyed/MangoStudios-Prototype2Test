﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private BulletModel model;
	private float speed;
	private int playerType;
	private float clock;


	// Use this for initialization
	public void init(playerModel owner) {
		this.name = "Bullet";
		speed = 7;
		playerType = owner.getType ();
		clock = 0f;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BulletModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;

	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.up * Time.deltaTime * speed);
		clock = clock + Time.deltaTime;
		if (playerType == 0) {
			if (clock > 1.3) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 2) {
			if (clock > .9) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 1) {
			if (clock > .8) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 3) {
			if (clock > .6) {
				Destroy (this.gameObject);
			}
		}

		if (this.transform.position.x > 9 || this.transform.position.x < -9 || this.transform.position.y > 6 || this.transform.position.y < -6) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Boss") {
			Destroy (this.gameObject);
		}
	}
}
