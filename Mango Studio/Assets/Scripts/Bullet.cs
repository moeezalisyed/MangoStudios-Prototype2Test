using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private BulletModel model;
	private float speed;
	private int playerType;
	private float clock;



	// Use this for initialization
	public void init(playerModel owner) {
		print ("Created BUllet: " + owner.owner.playerType);
		if (owner.owner.usingcircpowerup == true || owner.owner.tag == "inviscircle") {
			if (owner.owner.usingcircpowerup == true) {
				print ("With powerup");
			}
			this.name = "SpecialBullet";
		} else {
			this.name = "Bullet";
		}



		playerType = owner.getType ();
		if (owner.owner.m.inSlowDown) {
			speed -= 6;
		}

		if (this.playerType == 0) {
			speed = 9.5f;
		} else if (this.playerType == 1) {
			speed = 8f;
		} else if (this.playerType == 2) {
			speed = 6.5f;
		}

		//speed += 3;

		if (this.name == "SpecialBullet") {
			speed -= 1.5f;
		}
		if (owner.owner.m.inSlowDown) {
			speed -= 1;
		}


		clock = 0f;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BulletModel>();					// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		playerbody.isTrigger = true;
		if (this.name == "Bullet") {
			transform.localScale = new Vector3 (.3f, 0.8f, 1);
		} else {
			transform.localScale = new Vector3 (.65f, 0.8f, 1);
		}

	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.up * Time.deltaTime * speed);

		//Special Bullets don't go out of range
		if (this.name != "SpecialBullet") {
			clock = clock + Time.deltaTime;
		}

		//The rest are to check if bullet is out of range

		if (playerType == 0) {
			if (clock >  .5f) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 2) {
			if (clock > 2f) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 1) {
			if (clock > .7f) {
				Destroy (this.gameObject);
			}
		}


		if (this.transform.position.x > 9 || this.transform.position.x < -9 || this.transform.position.y > 6 || this.transform.position.y < -6) {
			Destroy (this.gameObject);
		}
	}

	void OnBecomeInvisible(){
		Destroy (this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Boss" || other.name == "BossShield") {
			Destroy (this.gameObject);
		}
		if (other.name == "BossBeam" && this.name == "Bullet") {
			Destroy (this.gameObject);
		}
//		if (other.name == "BossBullet") {
//			Destroy (other.gameObject);
//			Destroy (this.gameObject);
//		}
	}
}
