using UnityEngine;
using System.Collections;

public class BossBeam : MonoBehaviour {

	private BossBeamModel model;
	private Boss m;
	private float speed;

	// Use this for initialization
	public void init (Boss owner) {
		this.name = "BossBeam";
		speed = 6;
		m = owner;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossBeamModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (2.2f, .2f, 1);

		this.transform.rotation = new Quaternion(m.transform.rotation .x,m.transform.rotation.y,m.transform.rotation.z,m.transform.rotation.w);



	}

	// Update is called once per frame
	void Update () {

		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.x > 7 || this.transform.position.x < -7 || this.transform.position.y > 5 || this.transform.position.y < -5) {
			Destroy (this.gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Destroy (this.gameObject);
		}
	}
}
