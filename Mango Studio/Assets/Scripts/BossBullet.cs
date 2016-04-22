using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {
	
	private BossBulletModel model;
	private float speed;
	private Boss owner;

	// Use this for initialization
	public void init (Boss boss) {
		owner = boss;
		this.name = "BossBullet";
		speed = owner.chargeSpeed;



		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossBulletModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D playerbody = gameObject.AddComponent<BoxCollider2D> ();
		playerbody.isTrigger = true;
		transform.localScale = new Vector3 (.35f, .35f, 1);
		this.owner.m.bulletsFolder.Add (this.model.gameObject);
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!owner.m.inSlowDown) {
			this.speed = owner.chargeSpeed;
		}

		transform.Translate (Vector3.up * Time.deltaTime * speed);

		if (this.transform.position.x > 7 || this.transform.position.x < -7 || this.transform.position.y > 5 || this.transform.position.y < -5) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		print ("entered collider in boss bullet");
		if (other.tag == "Player" || other.tag == "inviscircle") {
			Destroy (this.gameObject);
		}
	}
}
