using UnityEngine;
using System.Collections;

public class BossBeam : MonoBehaviour {

	private BossBeamModel model;
	private Boss m;
	private float speed;
	private int health;

	// Use this for initialization
	public void init (Boss boss) {
		this.name = "BossBeam";
		m = boss;
		speed = m.chargeSpeed*1.1f;
		health = 2;
	

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossBeamModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (1.8f, 0.2f, 1);
		this.GetComponent<BoxCollider2D> ().size = model.transform.localScale;
		this.transform.rotation = new Quaternion(m.transform.rotation .x,m.transform.rotation.y,m.transform.rotation.z,m.transform.rotation.w);
		this.m.m.bulletsFolder.Add (this.model.gameObject);


	}

	// Update is called once per frame
	void Update () {
		if (!m.m.inSlowDown) {
			this.speed = m.chargeSpeed;
		}

		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.x > 7 || this.transform.position.x < -7 || this.transform.position.y > 5 || this.transform.position.y < -5) {
			Destroy (this.gameObject);
		}

		if (health <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" || other.tag == "inviscircle") {
			Destroy (this.gameObject);
		}
		if (other.name == "Bullet") {
			health = health - 1;
		}
	}
}
