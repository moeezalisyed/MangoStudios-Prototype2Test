using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	private BulletModel model;
	private float speed;
	private int playerType;
	private float clock;



	// Use this for initialization
	public void init(playerModel owner) {
		if (owner.owner.usingcircpowerup == true || owner.owner.tag == "inviscircle") {
			this.name = "SpecialBullet";
		} else {
			this.name = "Bullet";
		}



		playerType = owner.getType ();


		if (this.playerType == 0) {
			speed = 8.5f;
		} else if (this.playerType == 1) {
			speed = 6;
		} else if (this.playerType == 2) {
			speed = 3.5f;
		}

		if (this.name == "SpecialBullet") {
			speed -= 3;
		}

		clock = 0f;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BulletModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		playerbody.isTrigger = true;

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
			if (clock >  2.0f) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 2) {
			if (clock > 3.5f) {
				Destroy (this.gameObject);
			}
		}
		else if (playerType == 1) {
			if (clock > 2.5f) {
				Destroy (this.gameObject);
			}
		}


		if (this.transform.position.x > 9 || this.transform.position.x < -9 || this.transform.position.y > 6 || this.transform.position.y < -6) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Boss" || other.name == "BossBeam") {
			Destroy (this.gameObject);
		}
		if (other.name == "BossBullet") {
			Destroy (other.gameObject);
			Destroy (this.gameObject);
		}
	}
}
