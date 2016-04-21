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
		speed = 6;
		playerType = owner.getType ();
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
//		else if (playerType == 3) {
//			if (clock > .6) {
//				Destroy (this.gameObject);
//			}
//		}

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
