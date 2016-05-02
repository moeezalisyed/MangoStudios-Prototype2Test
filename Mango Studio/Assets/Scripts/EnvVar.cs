using UnityEngine;
using System.Collections;

public class EnvVar : MonoBehaviour {

	public EnvVarModel model;
	private GameManager owner;

	private int health;
	

	// Use this for initialization
	public void init (GameManager m) {
		owner = m;
		this.name = "EnvVar";
		this.health = 5;

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the gem texture.

		model = modelObject.AddComponent<EnvVarModel>();						// Add a marbleModel script to control visuals of the gem.
		model.init(this);

		BoxCollider2D envbody = gameObject.AddComponent<BoxCollider2D> ();
		Rigidbody2D envrbody = gameObject.AddComponent<Rigidbody2D> ();
		envbody.isTrigger = true;
		envrbody.gravityScale = 0;
		model.offset ();
		//transform.localScale = new Vector3 (.35f, .35f, 1);
		//transform.localPosition -= new Vector3(-2f, -2f, 0);
		//this.owner.m.envFolder.Add (this.model.gameObject);

	}

	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			this.killThisEnv ();
		}
	}

	void doDamage(int x){
		this.health -= x;
		if (health == 4) {
			model.transform.localScale = new Vector3 (0.85f, 0.85f, 1);
			this.GetComponent<BoxCollider2D>().transform.localScale = model.transform.localScale;
		} else if (health == 3) {
			model.transform.localScale = new Vector3 (0.70f, 0.70f, 1);
			this.GetComponent<BoxCollider2D>().transform.localScale = model.transform.localScale;
		} else if (health == 2) {
			model.transform.localScale = new Vector3 (0.50f, 0.50f, 1);
			this.GetComponent<BoxCollider2D>().transform.localScale = model.transform.localScale;
		} else if (health == 1) {
			model.transform.localScale = new Vector3 (0.25f, 0.25f, 1);
			this.GetComponent<BoxCollider2D>().transform.localScale = model.transform.localScale;
		}else if (health <= 0) {
			this.killThisEnv ();
		}







	}

	void killThisEnv(){
		//Kill this and spawn a new one somewher else
		this.owner.spawnNewEnv();
		Destroy(this.model.gameObject);
		Destroy (this.gameObject);
	}


	void OnTriggerEnter2D(Collider2D other){
		//print ("entered collider in boss bullet");
		if (other.tag == "Player" || other.tag == "inviscircle") {
			//Destroy (this.gameObject);
		} else if (other.name == "Bullet" || other.name == "SpecialBullet") {
			// When hit by a bullet
			Destroy (other.gameObject);
		} else if (other.name == "BossBullet") {
			// When hit by a bossBullet
			Destroy (other.gameObject);
			this.doDamage (1);

		} else if (other.name == "BossBeam") {
			// When hit by a BossBeam
			Destroy (other.gameObject);
			this.doDamage (1);
		} else if (other.name == "TracerBullet") {
			Destroy (other.gameObject);
			this.doDamage (1);
		} else if (other.name == "Boss" || other.name == "BossBlade") {
			this.doDamage (1);
		}
	}
}
