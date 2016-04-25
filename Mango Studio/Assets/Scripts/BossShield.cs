using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BossShield : MonoBehaviour {

	private Boss m;
	private int ShieldHealth;
	public BossShieldModel model;
	private float clock1 = 0;
	private float clock2;

	// Use this for initialization
	public void init (Boss owner){
		this.name = "BossShield";

		m = owner;
		ShieldHealth = 30;
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.
		model = modelObject.AddComponent<BossShieldModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);
		BoxCollider2D bossbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D bossRbody = gameObject.AddComponent<Rigidbody2D> ();
		bossRbody.gravityScale = 0;
		bossbody.isTrigger = true;

		transform.localScale = new Vector3 (1.3f, 1.3f, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (.71f * Mathf.Cos (clock1), .71f * Mathf.Sin (clock1), 0);
		transform.eulerAngles = new Vector3 (0, 0, clock2*90 +225);
		clock1 = clock1 + (Time.deltaTime)*3.062f *.5f;
		clock2 = clock2 + (Time.deltaTime)*1.95f * .5f;

		if (this.ShieldHealth <= 0) {
			m.shieldDead = true;
			m.shieldcharge = 2f;
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "Bullet") {
			ShieldHealth = ShieldHealth - 2;
		} else if (other.name == "SpecialBullet") {
			ShieldHealth = ShieldHealth - 7;
		}
	}


}
